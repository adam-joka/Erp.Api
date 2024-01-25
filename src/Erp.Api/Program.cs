using Erp.Trucks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTrucksModule();

var app = builder.Build();

app.UseTrucksModuleEndpoints();

app.Run();