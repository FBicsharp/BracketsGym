using Microsoft.JSInterop;

namespace Gym.ViewModel
{
    public interface IAlphabethViewModel
    {
		string CurrentString { get; set; }
		Action StateHasChenged { get; set; }
		Task AddAlphabethStringAsync();
        Task ProcessAlphabethStringAsync();
        List<string> GetAlphabethRequestString();
        List<string> GetAlphabethResponseString();
        Task GeneratePDFAsync();
		Task RemoveStrings(int index);
		void ClearAll();
	}
}