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

#if DEBUG
builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
#endif
var app = builder.Build();

#if DEBUG
	app.UseSwagger();
	app.UseSwaggerUI();
#endif


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
