using CleaningBracketsAPI.Logic;

namespace CleaningBracketsAPI.Test
{
	public class BracketsCleanerTest
	{
		private BracketsCleaner _bracketsCleaner;
		private string _brackets;

        public BracketsCleanerTest()
        {
			_bracketsCleaner = new BracketsCleaner();
			_brackets= "()";
        }

        [Theory]
		[InlineData("(abc)", "abc")]
		[InlineData("((abc))", "abc")]
		[InlineData("(abc", "(abc")]
		[InlineData("()", "")]
		[InlineData("(ab) (cd)", "(ab) (cd)")]
		[InlineData("((ab) (cd))", "(ab) (cd)")]
		[InlineData("ab(cd)", "ab(cd)")]		
		[InlineData("ab)cd(", "ab)cd(")]
		[InlineData(")", ")")]
		[InlineData("(", "(")]
		[InlineData("", "")]
		public void ShouldCleanStringAndReturned(string inputStrings, string expectedResults)
		{
			// Arrange
			//is allready done in constructor

			// Act
			var ListOfStrings = new List<string>(){ inputStrings};
			var result = _bracketsCleaner.ProcessString(ListOfStrings, _brackets).First();

			// Assert
			Assert.Equal(result, expectedResults);

		}

		[Theory]
		[InlineData("(abc)", true)]
		[InlineData("((abc))", true)]		
		[InlineData("()", true)]
		[InlineData("(ab) (cd)", true)]
		[InlineData("((ab) (cd))", true)]
		[InlineData("ab(cd)", true)]
		[InlineData("(", false)]
		[InlineData(")", false)]
		[InlineData("ab(cd", false)]	
		[InlineData("abcd)", false)]
		[InlineData("abcd))", false)]
		[InlineData("(abcd))", false)]
		public void Check_If_Brackeths_Is_Balanced(string inputStrings, bool expectedResults)
		{
			// Arrange
			//is allready done in constructor

			// Act			
			var result = _bracketsCleaner.IsBracketsBalanced(inputStrings, _brackets);

			// Assert
			Assert.Equal(result, expectedResults);

		}




	}
}