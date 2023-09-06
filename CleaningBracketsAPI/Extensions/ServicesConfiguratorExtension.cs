using CleaningBracketsAPI.Logic;
using CleaningBracketsAPI.Logic.Pdf;
using Wkhtmltopdf.NetCore;

namespace CleaningBracketsAPI.Extensions
{
	public static class ServicesConfiguratorExtension
	{
		
		/// <summary>
		/// Configures CORS policy
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection ConfigureAllServices(this IServiceCollection services)
		{

			services.AddTransient<IBracketsCleaner,BracketsCleaner>();
			services.AddTransient<IPairsEnCleaner,PairsEnCleaner>();
			services.AddTransient<IStringMapsGenerator, StringMapsGenerator>();
			services.AddTransient<IPdfGenerator,PdfGenerator>();
			//services.AddWkhtmltopdf();
			services.AddSingleton<IPdfHtmlGenerator, PdfHtmlGenerator>();
			
			return services;
		}

	

	}
}
