using Microsoft.AspNetCore.Components.RenderTree;
using System.Text;



namespace CleaningBracketsAPI.Logic.Pdf
{
    public class PdfHtmlGenerator : IPdfHtmlGenerator
    {
        private readonly ILogger<PdfHtmlGenerator> logger;

        public PdfHtmlGenerator(ILogger<PdfHtmlGenerator> logger)
        {
            this.logger = logger;
        }
		public string GenerateHTMLTableFromMatirx(char[,] matrix)
		{
			int numRows = matrix.GetLength(0);
			int numCols = matrix.GetLength(1);

			StringBuilder htmlTable = new StringBuilder();
			htmlTable.AppendLine("<!DOCTYPE html>");
			htmlTable.AppendLine("<html>");
			htmlTable.AppendLine("<head>");
			htmlTable.AppendLine("<style>");
			htmlTable.AppendLine("table { border-collapse: collapse;\r\n }\r\n");
			htmlTable.AppendLine("td {\r\nwidth: 30px;\r\nheight: 30px;\r\ntext-align: center;\r\nvertical-align: middle;\r\ntransform-origin: center center;\r\n \r\npadding: 7px 7px 7px 7px;\r\n}\r\n");
			htmlTable.AppendLine(".rotate-90 {\r\ntransform: rotate(90deg);\r\n}\r\n");
			htmlTable.AppendLine(".rotate-180 {\r\ntransform: rotate(180deg);\r\n}\r\n");
			htmlTable.AppendLine(".rotate-270 {\r\ntransform: rotate(270deg);\r\n}\r\n");
			htmlTable.AppendLine(".rounded {\r\nborder-style: dashed none dashed none;\r\n}\r\n");
			htmlTable.AppendLine("body { font-family: Verdana, sans-serif;\r\n    margin: 0;\r\n    padding: 0;\r\n    height: 100vh;\r\n    display: flex;\r\n    justify-content: center;\r\n    align-items: center;\r\n}\r\n\r\n.center-container {\r\n    text-align: center;\r\n}");
			htmlTable.AppendLine("</style>");
			htmlTable.AppendLine("</head>");
			htmlTable.AppendLine("<body>");
			htmlTable.AppendLine("<div class=\"center-container\">");
			
			htmlTable.AppendLine("<table>");

			for (int row = 0; row < numRows; row++)
			{
				htmlTable.AppendLine("<tr>");

				for (int col = 0; col < numCols; col++)
				{
					char cellContent = matrix[row, col];
					//trovo il livello della matrice per capire di quanti gradi devo ruotare il carattere
					int level = 0;
					int degrees = level * 90;
					string rotationClass = $"class=\"rotate-{degrees} \"";					
					htmlTable.AppendFormat($"<td {rotationClass} >{cellContent}</td>");					
				}
				htmlTable.AppendLine("</tr>");
			}

			htmlTable.AppendLine("</table>");
			htmlTable.AppendLine("<div/>");
			htmlTable.AppendLine("</body>");
			htmlTable.AppendLine("</html>");

			return htmlTable.ToString();
		}

	}
}

