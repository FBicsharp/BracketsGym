using CleaningBracketsAPI.Logic;
using CleaningBracketsAPI.Logic.Pdf;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<BracketsCleaner>();
//builder.Services.AddTransient<PairsEnCleaner>();
builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<PdfGenerator>();
builder.Services.AddSingleton<IPdfHtmlGenerator,PdfHtmlGenerator>();

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

app.MapPost("/topdf", (List<string> inputString, HttpContext context) =>
{
	var pdfGenerator = context.RequestServices.GetRequiredService<PdfGenerator>();
	var PdfStream = pdfGenerator.GeneratePdf(inputString);
	if (PdfStream.Length== 0)
		return Results.BadRequest();
	return Results.File(PdfStream, "application/pdf", "testPDF.pdf");
	
});





app.Run();
