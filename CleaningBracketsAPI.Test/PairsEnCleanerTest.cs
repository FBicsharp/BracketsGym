using CleaningBracketsAPI.Logic;

namespace CleaningBracketsAPI.Test
{
	public class PairsEnCleanerTest
	{
		private PairsEnCleaner _pairsEnCleaner;
		private string _brackets;

        public PairsEnCleanerTest()
        {
			_pairsEnCleaner = new PairsEnCleaner();
        }

        [Theory]
		[InlineData("man", "a")]
		[InlineData("keep", "ee")]
		[InlineData("gqwertyuioplkjhgfdsazxcvbnm:?t", "qwertyuioplkjhgfdsazxcvbnm:?")]
		[InlineData("abcdefghijklmnopqrstuvwxyz", "efghijklmnopqrstuv")]
		
		public void ShouldCleanString(string inputStrings, string expectedResults)
		{
			// Arrange
			//is allready done in constructor

			// Act
			var ListOfStrings = new List<string>(){ inputStrings};
			var result = _pairsEnCleaner.ProcessString(ListOfStrings).First();

			// Assert
			Assert.Equal(result, expectedResults);
		}
	}
}