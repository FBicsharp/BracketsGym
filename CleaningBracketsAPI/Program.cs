using CleaningBracketsAPI.Logic;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(policy =>
{
	policy.AddPolicy("CorsPolicy", opt => opt
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader()
	);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.MapPost("/cleanbrackets", async (List<string> inputString, HttpContext context) =>
{
	
	var logger = context.RequestServices.GetRequiredService<ILogger<BracketsCleaner>>();
	var endpoint = context.Request.Path;
	var ipAddress = context.Connection.RemoteIpAddress;
	logger.LogInformation($"Request to endpoint {endpoint} from IP {ipAddress}\n ");
	var bracketsCleaner = new BracketsCleaner();
	return await bracketsCleaner.ProcessStringAsync(inputString);	
});

app.MapPost("/cleanpairs-en", async (List<string> inputString, HttpContext context) =>
{
	var logger = context.RequestServices.GetRequiredService<ILogger<PairsEnCleaner>>();
	var endpoint = context.Request.Path;
	var ipAddress = context.Connection.RemoteIpAddress;
	logger.LogInformation($"Request to endpoint {endpoint} from IP {ipAddress}\n ");
	
	var pairsEnCleaner = new PairsEnCleaner();
	return await pairsEnCleaner.ProcessStringAsync(inputString);
});

app.Run();
