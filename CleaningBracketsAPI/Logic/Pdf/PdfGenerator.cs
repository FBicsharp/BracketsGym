using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text;



namespace CleaningBracketsAPI.Logic.Pdf
{
    public class PdfGenerator
    {
        private readonly ILogger<PdfGenerator> logger;
        private readonly IHttpContextAccessor context;
		private readonly IPdfHtmlGenerator _htmlGenerator;

		public PdfGenerator(ILogger<PdfGenerator> logger, IHttpContextAccessor context, IPdfHtmlGenerator htmlGenerator)
        {
            this.logger = logger;
            this.context = context;
            _htmlGenerator = htmlGenerator;
        }

        /// <summary>
        /// Remove the external letter if matching
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public MemoryStream GeneratePdf(List<string> inputString)
        {
            var endpoint = context.HttpContext.Request.Path;
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
            logger.LogInformation($"Request to endpoint {endpoint} from IP {ipAddress}");
            var html = FormatString(inputString);

			
			var Renderer = new ChromePdfRenderer();
			Renderer.RenderingOptions.InputEncoding = Encoding.UTF8;
			var Pdf = Renderer.RenderHtmlAsPdf(html);					
            return Pdf.Stream;
        }

        private string FormatString(List<string> inputString)
        {
            var longhestString = inputString.OrderByDescending(x => x.Count()).First().Length + 1;
            var contentMaps = new StringMapsGenerator(longhestString, longhestString);
			return _htmlGenerator.GenerateHTMLTableFromMatirx(contentMaps.Generate(inputString));
		}
	}
}

