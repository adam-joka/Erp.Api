using Erp.Employees;
using Erp.Trucks;

var builder = WebApplication.CreateBuilder(args);

// modules start
builder.Services.AddTrucksModule();
builder.Services.AddEmployeesModule();

// modules end

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// modules start
app.UseTrucksModuleEndpoints();
app.UseEmployeesModuleEndpoints();
// modules end

app.MapSwagger();
app.UseSwaggerUI();

app.Run();