using Bubble.API.Profiles;
using Bubble.Data;
using Bubble.Service.Interfaces;
using Bubble.Service.Query;
using Bubble.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var NewsArticlesFrontEnd = "_newsArticlesFrontEnd";

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: NewsArticlesFrontEnd,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7291")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(GetAllArticlesQuery).Assembly);
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<NewsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlSrvr"),
        x => x.MigrationsAssembly("Bubble.API"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(NewsArticlesFrontEnd);

app.UseAuthorization();

app.MapControllers();

app.Run();
