using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.Api.Interfaces;
using PhotoAlbum.Api.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();


//Dependencies
builder.Services.AddTransient<IAlbumService, AlbumService>();
builder.Services.AddTransient<IPhotoService, PhotoService>();
builder.Services.AddTransient<IUserPhotoAlbumService, UserPhotoAlbumService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
