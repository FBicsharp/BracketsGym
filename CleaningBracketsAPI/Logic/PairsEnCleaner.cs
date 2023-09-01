using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text;

namespace CleaningBracketsAPI.Logic
{
	public class PairsEnCleaner
	{
		string[] defaultPatterns = {
			"az",
			"by",
			"cx",
			"dw",
			"iv",
			"fu",
			"gt",
			"hs",
			"ir",
			"jq",
			"kp",
			"lo",
			"mn"
		};

		/// <summary>
		/// Remove the external letter if matching
		/// </summary>
		/// <param name="strings"></param>
		/// <returns></returns>
		public IEnumerable<string> ProcessString(IEnumerable<string> inputString, string[] patterns = null)
		{

			if (patterns is null)
				patterns = defaultPatterns;//force default parameters
			var result = new List<string>();
			foreach (var s in inputString)
			{
				var tmpString= s;
				foreach (var pattern in patterns)
				{
					tmpString = RemoveExtenalCharacter(tmpString, pattern);
				}
				result.Add(tmpString);
			}

			return result;
		}
		/// <summary>
		/// Remove the external letter if matching
		/// </summary>
		/// <param name="strings"></param>
		/// <returns></returns>
		public async Task<IEnumerable<string>> ProcessStringAsync(IEnumerable<string> strings, string[] patterns = null)
			=> await Task.FromResult(ProcessString(strings, patterns));

		/// <summary>
		/// Remove the external letter if matching
		/// </summary>
		/// <param name="inputString"></param>
		/// <param name="pattern"></param>
		/// <returns></returns>
		public string RemoveExtenalCharacter(string inputString, string pattern)
		{
			var result = inputString;
			if (pattern.Length < 2)
				return result;


			if (result.Length >= 2 &&
				 result[0] == pattern[0] && result[result.Length - 1] == pattern[1]
				)
			{
				return result.Remove(result.Length - 1, 1).Remove(0, 1);
			}
			return result;
		}

	}


}

