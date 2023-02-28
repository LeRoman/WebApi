using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApiRadency.MappingProfile;
using WebApiRadency.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<BooksDbContext>(opt =>
opt.UseInMemoryDatabase("BooksList"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(BookProfile));


var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
