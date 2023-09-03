namespace CleaningBracketsAPI.Logic.Pdf
{
	public interface IPdfHtmlGenerator
	{
		/// <summary>
		/// Generate a HTML table from a matrix of char
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns>string as html content</returns>
		string GenerateHTMLTableFromMatirx(char[,] matrix);		

	}
}