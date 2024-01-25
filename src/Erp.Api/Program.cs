using Erp.Trucks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTrucksModule();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseTrucksModuleEndpoints();

app.MapSwagger();
app.UseSwaggerUI();

app.Run();