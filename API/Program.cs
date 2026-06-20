using API.Endpoint;
using Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.RegisterApplicationServices();


var dbName = builder.Configuration.GetValue<string>("Database");
builder.Services.RegisterInfrastructureDependencies(dbName);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.RegisterUserEndpoints();

await app.RunAsync();