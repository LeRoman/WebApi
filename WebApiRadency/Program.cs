using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApiRadency.MappingProfile;
using WebApiRadency.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<BooksDbContext>(opt =>
opt.UseInMemoryDatabase("BooksList"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(BookProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();




app.Run();
