using System;
using System.Text;
using Bubble.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bubble.Data.Migrations
{
    public partial class Prefill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Comments",
                newName: "PostTime");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            Guid adminGuid = Guid.NewGuid();

            migrationBuilder.InsertData(
                table: "AccessRoles",
                columns: new[] {"Id", "Name"},
                values: new object[] {
                    adminGuid,
                    "Administrator"
                });

            migrationBuilder.InsertData(
                table: "AccessRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] {
                    Guid.NewGuid(),
                    "Editor"
                });

            migrationBuilder.InsertData(
                table: "AccessRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] {
                    Guid.NewGuid(),
                    "Reader"
                });

            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Data = sha1.ComputeHash(Encoding.UTF8.GetBytes("#*sjfngua;123nf@&_admin"));
            var hashedPassword = Encoding.UTF8.GetString(sha1Data);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Email", "EncryptedPassword", "RoleId" },
                values: new object[] {
                    Guid.NewGuid(),
                    "Admin",
                    "Admin.dont@memail.com",
                    hashedPassword,
                    adminGuid
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Title", "ArticleText", "Source", "PublishDate", "GoodnessIndex" },
                values: new object[] {
                    Guid.NewGuid(),
                    "TestArticle 1",
                    @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec tempus maximus ligula quis luctus. Ut consequat mi mauris, nec pellentesque ipsum interdum sit amet. Mauris porttitor a leo id ultricies. Pellentesque aliquet feugiat justo ac dignissim. Suspendisse porta fringilla elit iaculis dignissim. Pellentesque ut pellentesque tortor, sit amet faucibus erat. Curabitur eget aliquam turpis. Sed ultricies velit velit, ac pharetra leo sollicitudin aliquet. Duis consequat ornare commodo. Phasellus quis orci quis purus interdum rutrum. Donec porta congue metus sed vulputate. Duis eleifend leo augue, in cursus metus efficitur id. Pellentesque placerat commodo condimentum. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Praesent ultrices at dolor volutpat porttitor.

Nullam ut imperdiet nisi. Pellentesque faucibus turpis ut euismod vulputate. Mauris lorem purus, euismod et ipsum sit amet, maximus vestibulum ex. Donec velit felis, convallis a elementum et, accumsan eu lectus. Nulla non sem magna. Cras id neque blandit, ornare augue sit amet, lacinia mauris. Curabitur dignissim suscipit mi sit amet faucibus. Fusce erat felis, tempus nec varius nec, consectetur nec turpis. Nulla eu massa a mauris consectetur iaculis. Nulla molestie elementum elit, nec luctus felis scelerisque nec.

Vivamus imperdiet gravida nunc quis congue. Praesent laoreet feugiat ligula, nec aliquam dolor sagittis vel. Nullam aliquam malesuada orci sed fermentum. Nulla ullamcorper magna ut dictum auctor. Praesent id leo nunc. Mauris ac eros congue leo facilisis tristique sed vel magna. Morbi urna sapien, ultrices quis augue eu, viverra suscipit diam. Ut ac faucibus leo. Morbi eget mattis nulla. In sollicitudin nec dui in feugiat. Curabitur sapien purus, interdum eget interdum eget, convallis sed ante. Donec pretium risus gravida felis ornare, eu hendrerit massa ullamcorper. Nunc viverra sit amet sem in fringilla.",
                    "RandomGenerator",
                    DateTime.Now,
                    1m
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Title", "ArticleText", "Source", "PublishDate", "GoodnessIndex" },
                values: new object[] {
                    Guid.NewGuid(),
                    "TestArticle 2",
                    @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent vitae auctor libero. Vivamus dapibus odio nec ante fermentum, ut efficitur neque finibus. Nunc neque ipsum, iaculis in aliquet varius, tincidunt id dui. Fusce elementum aliquam lacinia. Cras nisl enim, dignissim in fermentum laoreet, rutrum ut magna. Aliquam tempor mollis tincidunt. Nulla dictum aliquet dolor, eget sollicitudin est ultrices ac. Vivamus bibendum tortor ut odio vulputate convallis. Cras ut pulvinar tellus. Nullam efficitur interdum bibendum. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin justo nulla, blandit at sem sit amet, dignissim sodales eros.

Ut dictum tincidunt velit a convallis. Curabitur sollicitudin laoreet tortor, at egestas nunc facilisis convallis. Fusce euismod dui leo, dignissim tempor augue accumsan quis. Cras venenatis tortor quis elementum dictum. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce metus mauris, efficitur sed tincidunt eget, semper nec sapien. Aenean ac neque non sem dignissim interdum sollicitudin sit amet lorem. Morbi ligula est, pretium in elit ac, aliquam lacinia metus. Praesent lorem mi, congue id lacus eget, laoreet suscipit odio. Aenean quis mauris sed eros molestie placerat. Aenean tempus scelerisque fringilla. Quisque blandit, quam vel ullamcorper eleifend, orci mauris sagittis risus, non malesuada mi nisi eu velit. Nulla at nisi vehicula, convallis diam in, dignissim ante.

Nulla erat nulla, cursus et neque sit amet, feugiat viverra lectus. Phasellus ac justo purus. Praesent faucibus, justo sed malesuada ultrices, enim augue scelerisque magna, non eleifend mi est in neque. Integer diam turpis, porttitor in convallis et, ultricies in diam. Sed a ultrices sapien. Donec dictum interdum sapien sit amet condimentum. Quisque fringilla, odio non pulvinar eleifend, turpis turpis blandit diam, a porttitor libero lacus sed lacus. Morbi eget aliquet nisi. In ut consectetur ligula, vitae dictum est.

Vestibulum quis nunc varius, viverra ex ut, fermentum odio. Vivamus malesuada dignissim ultrices. Morbi nec scelerisque turpis. Nulla orci augue, porttitor vitae vehicula quis, volutpat vitae eros. Nam fringilla venenatis metus eget ornare. Morbi imperdiet lectus quis mauris volutpat tempor. Nullam augue urna, accumsan quis sollicitudin ac, faucibus vitae ipsum. Interdum et malesuada fames ac ante ipsum primis in faucibus. Aenean sed dui quis turpis ultricies mattis gravida vitae ipsum. Aenean diam quam, convallis eu magna in, ornare vehicula eros.",
                    "RandomGenerator",
                    DateTime.Now,
                    1m
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Title", "ArticleText", "Source", "PublishDate", "GoodnessIndex" },
                values: new object[] {
                    Guid.NewGuid(),
                    "TestArticle 3",
                    @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras vel quam rhoncus, rhoncus odio at, varius augue. Sed sem metus, porttitor ac condimentum quis, vulputate quis augue. Pellentesque congue felis lacus, vel pharetra leo porta varius. Vivamus lacinia molestie libero, eget auctor metus posuere et. Ut tristique purus metus, tincidunt maximus leo congue ac. Suspendisse porttitor maximus pharetra. Sed nec vulputate risus. Nunc sit amet turpis urna. Aenean imperdiet justo a ligula imperdiet euismod.

Maecenas ultricies neque tincidunt volutpat elementum. Proin sed odio felis. Duis nulla ante, sagittis at metus a, blandit semper nulla. Praesent quis fringilla nisl. Aliquam erat volutpat. Ut eros ipsum, convallis vel tristique ut, scelerisque dignissim dolor. Duis sed ante nisi. Curabitur ultricies turpis in neque volutpat malesuada quis eget justo. Curabitur erat libero, imperdiet non posuere id, condimentum sit amet tortor. Quisque molestie, ante et vulputate fermentum, ante tellus iaculis purus, in suscipit justo felis sit amet nisl. Sed eget lorem lacinia, blandit mauris et, pretium libero. Etiam non nisi nec neque eleifend blandit et eget augue. Phasellus id euismod lorem, fermentum pellentesque lorem. Vestibulum sem libero, vulputate nec maximus nec, tincidunt sit amet quam.",
                    "RandomGenerator",
                    DateTime.Now,
                    1m
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "PostTime",
                table: "Comments",
                newName: "DateTime");
        }
    }
}
