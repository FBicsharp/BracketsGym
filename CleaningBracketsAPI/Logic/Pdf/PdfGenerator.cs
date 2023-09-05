
//using BitMiracle.LibTiff.Classic;
//using System;
//using System.Drawing.Imaging;
//using System.Drawing.Printing;
//using WkHtmlToPdfDotNet;

namespace CleaningBracketsAPI.Logic.Pdf
{
	public class PdfGenerator : IPdfGenerator
	{
		private readonly ILogger<PdfGenerator> _logger;
		private readonly IHttpContextAccessor _context;
		private readonly IPdfHtmlGenerator _htmlGenerator;
		private readonly IStringMapsGenerator _stringMapsGenerator;

		public PdfGenerator(ILogger<PdfGenerator> logger, IHttpContextAccessor context, IPdfHtmlGenerator htmlGenerator, IStringMapsGenerator stringMapsGenerator)
		{
			_logger = logger;
			_context = context;
			_htmlGenerator = htmlGenerator;
			_stringMapsGenerator = stringMapsGenerator;
		}

		public byte[] GeneratePdfAndRetriveByte(List<string> inputString)
		{
			var endpoint = _context.HttpContext?.Request?.Path;
			var ipAddress = _context.HttpContext?.Connection?.RemoteIpAddress;
			_logger.LogInformation($"Request to endpoint {endpoint} from IP {ipAddress}");
			var PdfStream = new byte[0];
			try
			{
				ResizeEqualStringLengths(ref inputString);
				var longhestString = inputString.Max(x => x.Length);

				_stringMapsGenerator.Initialize(longhestString, longhestString);
				var html = _htmlGenerator.GenerateHTMLTableFromMatirx(_stringMapsGenerator.Generate(inputString));
				var htmlToPdf = new SelectPdf.HtmlToPdf();
				htmlToPdf.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
				var pdf = htmlToPdf.ConvertHtmlString(html);
				PdfStream = pdf.Save();
				pdf.Close();



				//var Renderer = new IronPdf.ChromePdfRenderer().RenderHtmlAsPdf(html);
				//PdfStream = Renderer.BinaryData;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during pdf generation");
			}

			return PdfStream;
		}
		public async Task<byte[]> GeneratePdfAndRetriveByteAsync(List<string> inputString)
			=> await Task.Factory.StartNew(() => GeneratePdfAndRetriveByte(inputString));

		/// <summary>
		/// Modify the length of strings by adding a space character to strings with equal lengths.
		/// </summary>
		/// <param name="inputStrings"></param>
		public void ResizeEqualStringLengths(ref List<string> inputStrings)
		{
			// Ordina la lista in modo crescente per lunghezza
			inputStrings = inputStrings.OrderBy(x => x.Length).ToList();
			var check = true;
			while (check)
			{
				check = false;
				for (int i = 0; i < inputStrings.Count; i++)
				{

					if ( i< inputStrings.Count-1 && inputStrings[i].Length >= inputStrings[i +1].Length)
					{
						// Aggiungi uno spazio all'inizio della stringa successiva
						inputStrings[i + 1] = " " + inputStrings[i+1];
						check = true;
					}
				}
			}
			inputStrings = inputStrings.OrderByDescending(x => x.Length).ToList();

		}

		

	}
}

