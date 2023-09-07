using CleaningBracketsAPI.Extensions;
using CleaningBracketsAPI.Logic.Pdf.wkhtmltopdf.Interfaces;
//using Microsoft.AspNetCore.Mvc;

namespace CleaningBracketsAPI.Logic.Pdf.wkhtmltopdf
{
	public class GeneratePdf : IGeneratePdf
	{
		protected IConvertOptions _convertOptions;

        public readonly ILogger<GeneratePdf> _logger;

		//readonly IRazorViewToStringRenderer _engine;

		public GeneratePdf(ILogger<GeneratePdf> logger)
		{
			_convertOptions = new ConvertOptions();
			_logger = logger;
		}

		public void SetConvertOptions(IConvertOptions convertOptions)
		{
			_convertOptions = convertOptions;
		}

		public byte[] GetPDF(string html)
		{
            try
            {
			    return WkhtmlDriver.Convert(WkhtmltopdfConfiguration.WKhtmltopdfPath, _convertOptions.GetConvertOptions(), html);
            }
            catch (Exception ex)
            {
				_logger.LogError("Error in GetPDF",ex);
            }
            return new byte[] {};
		}
		/*
        public async Task<byte[]> GetByteArray<T>(string View, T model)
        {
            try
            {
                var html = await _engine.RenderViewToStringAsync(View, model);
                return GetPDF(html);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> GetPdf<T>(string View, T model)
        {
            var html = await _engine.RenderViewToStringAsync(View, model);
            var byteArray = GetPDF(html);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public async Task<IActionResult> GetPdfViewInHtml<T>(string ViewInHtml, T model)
        {
            var html = await _engine.RenderHtmlToStringAsync(ViewInHtml, model);
            var byteArray = GetPDF(html);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public async Task<byte[]> GetByteArrayViewInHtml<T>(string ViewInHtml, T model)
        {
            try
            {
                var view = await _engine.RenderHtmlToStringAsync(ViewInHtml, model);
                return GetPDF(view);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddView(string path, string viewHTML) => _engine.AddView(path, viewHTML);

        public bool ExistsView(string path) => _engine.ExistsView(path);

        public void UpdateView(string path, string viewHTML) => _engine.UpdateView(path, viewHTML);

        public async Task<IActionResult> GetPdfViewInHtml(string ViewInHtml)
        {
            var html = await _engine.RenderHtmlToStringAsync(ViewInHtml);
            var byteArray = GetPDF(html);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public async Task<byte[]> GetByteArrayViewInHtml(string ViewInHtml)
        {
            try
            {
                var view = await _engine.RenderHtmlToStringAsync(ViewInHtml);
                return GetPDF(view);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> GetPdf(string View)
        {
            var html = await _engine.RenderViewToStringAsync(View);
            var byteArray = GetPDF(html);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public async Task<byte[]> GetByteArray(string View)
        {
            try
            {
                var html = await _engine.RenderViewToStringAsync(View);
                return GetPDF(html);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */
	}
}