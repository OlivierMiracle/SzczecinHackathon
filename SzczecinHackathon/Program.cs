using Microsoft.EntityFrameworkCore;
using SzczecinHackathon.Controllers;
using SzczecinHackathon.Data;
using SzczecinHackathon.Services;
using SzczecinHackathon.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(@"Data Source =.\\AppDB.db;");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IFriendsService, FriendService>();
builder.Services.AddScoped<IHappeningService, HappeningService>();

var app = builder.Build();

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
