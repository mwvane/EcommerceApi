using Microsoft.EntityFrameworkCore;
using EcommerceApp.Models;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;
using EcommerceApp.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Context>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("context")));
builder.Services.AddControllers().AddJsonOptions(
            options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
        );

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
     builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
     {
         build.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
     }));

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("corspolicy");
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
                           Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.Run();
