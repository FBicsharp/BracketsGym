using CleaningBracketsAPI.Logic.Pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Text;

namespace CleaningBracketsAPI.Test
{
    public class PdfHtmlGeneratorTest
	{
		private IPdfHtmlGenerator _pdfHtmlGenerator;

		public PdfHtmlGeneratorTest()
		{
			var loggerMock = Substitute.For<ILogger<PdfHtmlGenerator>>();
			var httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
			_pdfHtmlGenerator = new PdfHtmlGenerator(loggerMock);
		}

		//[Fact]
		//public void should_return_html_content_without_table_css_class_apllyied()
		//{
		//	// Arrange
		//	//is allready done in constructor
		//	//Act			
		//	char[,] matrinxChar = new char[,]
		//	{
		//		{'A', 'B', 'C', 'D', 'E', 'F'},
		//		{'G', 'H', 'I', 'J', 'K', 'R'},
		//		{'M', 'N', 'O', 'P', 'Q', 'X'},
		//		{'S', 'T', 'U', 'V', 'W', '4'},
		//		{'Y', 'Z', '1', '2', '3', '4'},
		//		{'5', '6', '7', '8', '9', '5'}
		//	};
		//	string expectedResults = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n<style>\r\ntable { border-collapse: collapse;\r\n }\r\n\r\ntd {\r\nwidth: 30px;\r\nheight: 30px;\r\ntext-align: center;\r\nvertical-align: middle;\r\ntransform-origin: center center;\r\n \r\npadding: 7px 7px 7px 7px;\r\n}\r\n\r\n.rotate-90 {\r\ntransform: rotate(90deg);\r\n}\r\n\r\n.rotate-180 {\r\ntransform: rotate(180deg);\r\n}\r\n\r\n.rotate-270 {\r\ntransform: rotate(270deg);\r\n}\r\n\r\n.rounded {\r\nborder-style: dashed none dashed none;\r\n}\r\n\r\nbody { font-family: Verdana, sans-serif;\r\n    margin: 0;\r\n    padding: 0;\r\n    height: 100vh;\r\n    display: flex;\r\n    justify-content: center;\r\n    align-items: center;\r\n}\r\n\r\n.center-container {\r\n    text-align: center;\r\n}\r\n</style>\r\n</head>\r\n<body>\r\n<div class=\"center-container\">\r\n<table>\r\n<tr>\r\n<td class=\"rotate-0 \" >A</td><td class=\"rotate-0 \" >B</td><td class=\"rotate-0 \" >C</td><td class=\"rotate-0 \" >D</td><td class=\"rotate-0 \" >E</td><td class=\"rotate-0 \" >F</td></tr>\r\n<tr>\r\n<td class=\"rotate-0 \" >G</td><td class=\"rotate-0 \" >H</td><td class=\"rotate-0 \" >I</td><td class=\"rotate-0 \" >J</td><td class=\"rotate-0 \" >K</td><td class=\"rotate-0 \" >R</td></tr>\r\n<tr>\r\n<td class=\"rotate-0 \" >M</td><td class=\"rotate-0 \" >N</td><td class=\"rotate-0 \" >O</td><td class=\"rotate-0 \" >P</td><td class=\"rotate-0 \" >Q</td><td class=\"rotate-0 \" >X</td></tr>\r\n<tr>\r\n<td class=\"rotate-0 \" >S</td><td class=\"rotate-0 \" >T</td><td class=\"rotate-0 \" >U</td><td class=\"rotate-0 \" >V</td><td class=\"rotate-0 \" >W</td><td class=\"rotate-0 \" >4</td></tr>\r\n<tr>\r\n<td class=\"rotate-0 \" >Y</td><td class=\"rotate-0 \" >Z</td><td class=\"rotate-0 \" >1</td><td class=\"rotate-0 \" >2</td><td class=\"rotate-0 \" >3</td><td class=\"rotate-0 \" >4</td></tr>\r\n<tr>\r\n<td class=\"rotate-0 \" >5</td><td class=\"rotate-0 \" >6</td><td class=\"rotate-0 \" >7</td><td class=\"rotate-0 \" >8</td><td class=\"rotate-0 \" >9</td><td class=\"rotate-0 \" >5</td></tr>\r\n</table>\r\n<div/>\r\n</body>\r\n</html>\r\n\r\n";

		//	var result = "";_pdfHtmlGenerator.GenerateHTMLTableFromMatirx(matrinxChar);

		//	// Assert
		//	Assert.Equal(result, expectedResults);
		//}

		


	}
}