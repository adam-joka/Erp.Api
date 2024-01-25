using Erp.Trucks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTrucksModule();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseTrucksModuleEndpoints();

app.UseSwaggerUI();

app.Run();