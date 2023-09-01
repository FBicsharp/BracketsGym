using System.Text;

namespace CleaningBracketsAPI.Logic
{
	public class BracketsCleaner
	{

		/// <summary>
		/// Remove extra brackets from string
		/// </summary>
		/// <param name="strings"></param>
		/// <returns></returns>
		public IEnumerable<string> ProcessString(IEnumerable<string> inputString, string brackets = "()")
		{

			if (brackets.Count() < 2)
				brackets = "()";//force default brackets if not set or invalid

			var result = inputString.AsParallel()
						 .Select(s => RemoveExtenalExtraBrackets(s, brackets))
						 .ToList();
			return result;
		}

		public async Task<IEnumerable<string>> ProcessStringAsync(IEnumerable<string> strings, string brackets = "()")
			=> await Task.FromResult(ProcessString(strings, brackets));
				
		/// <summary>
		/// Remove the external brackets if they are extra until the string has balanced brackets
		/// </summary>
		/// <param name="inputString"></param>
		/// <param name="brackets"></param>
		/// <returns></returns>
		public string RemoveExtenalExtraBrackets(string inputString, string brackets)
		{
			
			var result = inputString;
			var bracketOpen = brackets[0];
			var bracketClose = brackets[1];

			if (result.Length >= 2 &&
				 result[0] == bracketOpen && result[result.Length - 1] == bracketClose
				)
			{
				var tmpResult = result.Remove(result.Length - 1, 1).Remove(0, 1);
				if (IsBracketsBalanced(tmpResult, brackets))
					result = RemoveExtenalExtraBrackets(tmpResult, brackets);
			}

			return result;
		}

		/// <summary>
		/// Check if the brackets are balanced in the string and return true if they are balanced
		/// </summary>
		/// <param name="inputString"></param>
		/// <param name="brackets"></param>
		/// <returns></returns>
		public bool IsBracketsBalanced(string inputString, string brackets)
		{
			var result = inputString;
			var bracketOpen = brackets[0];
			var bracketClose = brackets[1];
			var stringStack = new Queue<char>();

			for (int i = 0; i < inputString.Length; i++)
			{

				var currentChar = inputString[i]; //if the char is not a bracket then continue to the next char becouse we are looking for brackets

				if (currentChar != bracketOpen && currentChar != bracketClose)
					continue;

				if (currentChar == bracketOpen)//I can open multiple parenthesit ,I dont need to check if the first element is open or close
				{
					stringStack.Enqueue(currentChar);
					continue;
				}

				if (currentChar == bracketClose)
				{
					//checking the first element added if its not opposite of the current element 
					if (stringStack.Count == 0 || stringStack.Dequeue() != bracketOpen)
						return false;
					continue;
				}
			}
			return stringStack.Count() == 0;
		}
	}


}

