using MCI_NameTag_Services_POC.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TDataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NameTagCon")));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsProduction())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //    //c.RoutePrefix = string.Empty;
    //});
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
