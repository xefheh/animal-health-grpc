using AnimalHealth.API.Services;
using AnimalHealth.Application;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString == null) throw new ArgumentNullException(connectionString);

builder.Services.AddPersistenceLayer(opt => opt.UseNpgsql(connectionString));
builder.Services.AddApplicationLayer();
builder.Services.AddLogApplicationLayer();
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

app.MapGrpcService<AuthService>();
app.MapGrpcService<InspectionService>();
app.MapGrpcService<VaccinationService>();
app.MapGrpcService<OrganizationService>();
app.MapGrpcService<ContractService>();
app.MapGrpcService<OtherSourceService>();
app.MapGrpcService<ReportService>();

if (app.Environment.IsDevelopment()) app.MapGrpcReflectionService();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();