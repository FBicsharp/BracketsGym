﻿using CleaningBracketsAPI.Logic;
using CleaningBracketsAPI.Logic.Pdf;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.HttpsPolicy;

namespace CleaningBracketsAPI.Extensions
{
	public static class CorsConfiguratorExtension
	{
		public const string CorsPolicyName = "CorsPolicy";
		/// <summary>
		/// Configures CORS policy
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(policy =>
			{
				policy.AddPolicy(CorsPolicyName, opt => opt
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
				);
			});

			return services;
		}
	}
}
