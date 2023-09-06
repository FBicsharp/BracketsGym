using CleaningBracketsAPI.Logic;
using CleaningBracketsAPI.Logic.Pdf;

namespace CleaningBracketsAPI.Extensions
{
	public static class EndpointConfiguratorExtensions
	{



		/// <summary>		
		/// Adds all endpoint to the application
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		public static IApplicationBuilder MapAllEndpint(this WebApplication app)
		{

			app.MapPost("/cleanbrackets", async (List<string> inputString, HttpContext context) =>
			{
				var bracketsCleaner = context.RequestServices.GetRequiredService<IBracketsCleaner>();
				return await bracketsCleaner.ProcessStringAsync(inputString);
			});

			app.MapPost("/cleanpairs-en", async (List<string> inputString, HttpContext context) =>
			{
				var pairsEnCleaner = context.RequestServices.GetRequiredService<IPairsEnCleaner>();
				return await pairsEnCleaner.ProcessStringAsync(inputString);
			});

			app.MapPost("/topdf", async (List<string> inputString, HttpContext context) =>
			{
				var pdfGenerator = context.RequestServices.GetRequiredService<IPdfGenerator>();
				var PdfStream = await pdfGenerator.GeneratePdfAndRetriveByteAsync(inputString);
				var filename = "file.pdf";
				if (PdfStream.Length == 0 )
					return Results.StatusCode(204);
				return Results.File(PdfStream, "application/pdf", filename);

			});
			return app;
		}
	}
}
