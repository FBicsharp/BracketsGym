//using Microsoft.AspNetCore.Mvc;

namespace CleaningBracketsAPI.Logic.Pdf.wkhtmltopdf.Interfaces
{
	public interface IGeneratePdf
	{
		byte[] GetPDF(string html);
		/*
		Task<IActionResult> GetPdfViewInHtml<T>(string ViewInHtml, T model);
		Task<byte[]> GetByteArrayViewInHtml<T>(string ViewInHtml, T model);
		Task<IActionResult> GetPdf<T>(string View, T model);
		Task<byte[]> GetByteArray<T>(string View, T model);
		Task<IActionResult> GetPdfViewInHtml(string ViewInHtml);
		Task<byte[]> GetByteArrayViewInHtml(string ViewInHtml);
		Task<IActionResult> GetPdf(string View);
		Task<byte[]> GetByteArray(string View);
		void SetConvertOptions(IConvertOptions options);
		void UpdateView(string path, string viewHTML);
		bool ExistsView(string path);
		void AddView(string path, string viewHTML);*/
	}
}