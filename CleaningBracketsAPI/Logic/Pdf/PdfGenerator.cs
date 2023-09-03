
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
			var longhestString = inputString.Max(x => x.Length);
			var PdfStream = new byte[0];
			try
			{
				_stringMapsGenerator.Initialize(longhestString, longhestString);
				var html = _htmlGenerator.GenerateHTMLTableFromMatirx(_stringMapsGenerator.Generate(inputString));
				var htmlToPdf = new SelectPdf.HtmlToPdf();
				htmlToPdf.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
				var pdf = htmlToPdf.ConvertHtmlString(html);
				PdfStream = pdf.Save();
				pdf.Close();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during pdf generation");
			}

            return PdfStream;
		}
		public async Task<byte[]> GeneratePdfAndRetriveByteAsync(List<string> inputString)
			=> await Task.Factory.StartNew(() => GeneratePdfAndRetriveByte(inputString));


	}
}

