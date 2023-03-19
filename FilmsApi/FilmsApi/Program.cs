using FilmsApi.Controllers;
using FilmsApi.Filters;
using FilmsApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddResponseCaching();
// builder.Services.AddTransient LifeTime short for any request
// builder.Services.AddScoped LifeTime medium for any request inside my http context
// builder.Services.AddSingleton LifeTime long for all time app

builder.Services.AddScoped<IRepository, RepositoryInMemory>();
builder.Services.AddScoped<WeatherForecastController>();
builder.Services.AddTransient<MyActionFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();  

app.Map("/map1", (app) =>
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("I'm intercepting the pipeline");
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
