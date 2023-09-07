using CleaningBracketsAPI.Logic.Pdf.wkhtmltopdf;
using CleaningBracketsAPI.Logic.Pdf.wkhtmltopdf.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace CleaningBracketsAPI.Extensions
{
	public static class WkhtmltopdfConfiguration
	{
		public static string WKhtmltopdfPath { get; set; } = "PdfHelper";

		public static IServiceCollection AddWkhtmltopdfSimple(this IServiceCollection services, string wkhtmltopdfRelativePath = "PdfHelper")
		{
			WKhtmltopdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, wkhtmltopdfRelativePath);
			//get full path
			var tmp_path = Assembly.GetExecutingAssembly().Location;

			if (!Directory.Exists(WKhtmltopdfPath))
			{
				throw new Exception($"Missing file on Folder containing wkhtmltopdf not found, searched for {WKhtmltopdfPath} ");
			}
			services.AddTransient<IGeneratePdf, GeneratePdf>();

			return services;
		}
	}
}
