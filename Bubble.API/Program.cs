using Bubble.API.MapperProfiles;
using Bubble.Data;
using Bubble.CQS.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.SqlServer;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Bubble.APIServices.Interfaces;
using Bubble.APIServices.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var NewsArticlesFrontEnd = "_newsArticlesFrontEnd";

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: NewsArticlesFrontEnd,
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration.GetConnectionString("Blazor"))
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));

builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("SerilogLogs")
            , new MSSqlServerSinkOptions { TableName = "Logs" }));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfireServer();
builder.Services.AddMediatR(typeof(GetAllArticlesQuery).Assembly);
builder.Services.AddAutoMapper(typeof(UsersProfile).Assembly);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddDbContext<NewsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlSrvr"),
        x => x.MigrationsAssembly("Bubble.API"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseCors(NewsArticlesFrontEnd);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var articleService = scope.ServiceProvider.GetRequiredService<IArticleService>();
    RecurringJob.AddOrUpdate(
            "Fetch new articles",
                () => articleService.AddNewArticlesToDB(),
               Cron.Hourly);
    RecurringJob.AddOrUpdate(
                            "Rate some Articles",
                            () => articleService.RateUnratedArticlesGoodness(),
                            "0 */1 * * *"); //"*/5 * * * *"
    RecurringJob.AddOrUpdate(
            "Fetch articles text",
                () => articleService.FetchArticlesTextAsync(),
               Cron.Hourly);
}

app.Run();
