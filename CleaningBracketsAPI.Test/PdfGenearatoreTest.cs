using CleaningBracketsAPI.Logic;
using System.Text;

namespace CleaningBracketsAPI.Test
{
	public class PdfGenearatoreTest
	{
		private PairsEnCleaner _pairsEnCleaner;
		private string _brackets;

        public PdfGenearatoreTest()
        {
			_pairsEnCleaner = new PairsEnCleaner();
        }

        [Theory]
		[InlineData(new string[] { "Ciao", "bellissimo", "sds", "veve", "sadcsacasd" },
			"bellissimos\r\n          a\r\n          d\r\n          c\r\n          s\r\n          a\r\n          c\r\n      esdsa\r\n      v   s\r\n      e   d\r\n      voaiC")]
		public void ShouldCleanString(string[] inputStrings, string expectedResults)
		{
			// Arrange
			//is allready done in constructor
			//Act
			var result = CleanString(inputStrings);

			// Assert
			Assert.Equal(result, expectedResults);
		}
		public string CleanString(string[] inputStrings)
		{
			// Trova la lunghezza massima tra le stringhe nell'array
			int maxLength = inputStrings.Max(s => s.Length);
			inputStrings = inputStrings.OrderByDescending(x => x.Length).ToArray();

			// Inizializza una lista di stringhe modificate
			List<string> modifiedStrings = new List<string>();

			// Per ogni stringa nell'array, inverte la stringa e allineala a destra
			foreach (string input in inputStrings)
			{				
				string alignedString = input.PadLeft(maxLength);

				modifiedStrings.Add(alignedString);
			}

			// Unisci le stringhe modificate in un'unica stringa separata da una nuova riga
			string result = string.Join("\r\n", modifiedStrings);

			return result;
		}
	}
}