using CleaningBracketsAPI.Logic.Pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Text;

namespace CleaningBracketsAPI.Test
{
	public class PdfGenearatorTest
	{
		private PdfGenerator _pdfGenerator;

		public PdfGenearatorTest()
		{
			var loggerMock = Substitute.For<ILogger<PdfGenerator>>();
			var httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
			var pdfHtmlGenerator = Substitute.For<IPdfHtmlGenerator>();
			var stringMapsGenerator = Substitute.For<IStringMapsGenerator>();
			_pdfGenerator = new PdfGenerator(loggerMock, httpContextAccessorMock, pdfHtmlGenerator, stringMapsGenerator);
		}

		[Theory]
		[InlineData(new string[] { "STAMPA1", "STAMPA2", "STAMPA3", "STAMPA4" },
					new string[] { "   STAMPA4", "  STAMPA3", " STAMPA2", "STAMPA1"  }
		)]
		[InlineData(new string[] { "AAAA", "AAAA", "AAAA", "AAAA", "AAAA", "AAAA", "AAAA", "AAAA" },
					new string[] { "       AAAA", "      AAAA", "     AAAA", "    AAAA", "   AAAA", "  AAAA", " AAAA", "AAAA" }
		)]
		public void ShouldstretchStringIfLenghtIsduplicateString(string[] inputStrings, string[] expectedResults)
		{
			
			// Arrange
			//is allready done in constructor
			//Act			
			var list = inputStrings.ToList();
			_pdfGenerator.ResizeEquelsStingLenght(ref list);
			// Assert
			Assert.Equal(list, expectedResults);
		}


		



	}
}