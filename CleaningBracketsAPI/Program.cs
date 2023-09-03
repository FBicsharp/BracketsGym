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


app.MapGet("/test", (HttpContext contex) =>
{
	char[,] charMatrix = {
			{'A', 'B', 'C', 'D', 'E', 'F'},
			{'G', 'H', 'I', 'J', 'K', 'R'},
			{'M', 'N', 'O', 'P', 'Q', 'X'},
			{'S', 'T', 'U', 'V', 'W', '4',},
			{'Y', 'Z', '1', '2', '3', '4'},
			{'5', '6', '7', '8', '9', '5'}
		};
	var htmlGenerator = contex.RequestServices.GetRequiredService<IPdfHtmlGenerator>();
	var html = htmlGenerator.GenerateHTMLTableFromMatirx(charMatrix);
	//var html = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <title>Tabella 6x6 con Numeri Ruotati</title>\r\n    <style>\r\n        table {\r\n            border-collapse: collapse;\r\n        }\r\n        td {\r\n            width: 30px;\r\n            height: 30px;\r\n            text-align: center;\r\n            vertical-align: middle;\r\n            transform-origin: center center;\r\n        }\r\n        .rotate-90 {\r\n            transform: rotate(90deg);\r\n        }\r\n        .rotate-180 {\r\n            transform: rotate(180deg);\r\n        }\r\n        .rotate-270 {\r\n            transform: rotate(270deg);\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <table>\r\n        <tr>\r\n            <td>1</td>\r\n            <td class=\"rotate-270\">2</td>\r\n            <td class=\"rotate-270\">3</td>\r\n            <td class=\"rotate-270\">4</td>\r\n            <td class=\"rotate-270\">5</td>\r\n            <td class=\"rotate-90\">6</td>\r\n        </tr>\r\n        <tr>\r\n            <td>7</td>\r\n            <td>8</td>\r\n            <td>9</td>\r\n            <td>10</td>\r\n            <td>11</td>\r\n            <td class=\"rotate-90\">12</td>\r\n        </tr>\r\n        <tr>\r\n            <td class=\"rotate-270\">13</td>\r\n            <td>14</td>\r\n            <td>15</td>\r\n            <td>16</td>\r\n            <td>17</td>\r\n            <td class=\"rotate-90\">18</td>\r\n        </tr>\r\n        <tr>\r\n            <td class=\"rotate-270\">19</td>\r\n            <td>20</td>\r\n            <td>21</td>\r\n            <td>22</td>\r\n            <td>23</td>\r\n            <td class=\"rotate-90\">24</td>\r\n        </tr>\r\n        <tr>\r\n            <td class=\"rotate-270\">25</td>\r\n            <td>26</td>\r\n            <td>27</td>\r\n            <td>28</td>\r\n            <td>29</td>\r\n            <td class=\"rotate-90\">30</td>\r\n        </tr>\r\n        <tr>\r\n            <td class=\"rotate-270\">31</td>\r\n            <td class=\"rotate-180\">32</td>\r\n            <td class=\"rotate-180\">33</td>\r\n            <td class=\"rotate-180\">34</td>\r\n            <td class=\"rotate-180\">35</td>\r\n            <td class=\"rotate-180\">36</td>\r\n        </tr>\r\n    </table>\r\n</body>\r\n</html>\r\n";
	var Renderer = new ChromePdfRenderer();
	Renderer.RenderingOptions.InputEncoding = System.Text.Encoding.UTF8;
	using var Pdf = Renderer.RenderHtmlAsPdf(html);
	Pdf.SaveAs("HtmlString.pdf");
	var bytes = File.ReadAllBytes("HtmlString.pdf");
	if (bytes.Count() == 0)
		return Results.BadRequest();
	//assgno la stinga in bytes
	using (var stream = new MemoryStream())
	{
		return Results.File(bytes, "application/pdf", "generated.pdf");
	}
});



app.Run();
