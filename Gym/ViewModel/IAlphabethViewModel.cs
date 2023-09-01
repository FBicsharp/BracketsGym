namespace Gym.ViewModel
{
    public interface IAlphabethViewModel
    {
		string CurrentString { get; set; }
		Action StateHasChenged { get; set; }
        void AddAlphabethString();
        Task ProcessAlphabethStringAsync();
        List<string> GetAlphabethRequestString();
        List<string> GetAlphabethResponseString();
        
    }
}