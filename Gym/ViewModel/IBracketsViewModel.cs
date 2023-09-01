namespace Gym.ViewModel
{
    public interface IBracketsViewModel
	{
		string CurrentString { get; set; }
		Action StateHasChenged { get; set; }
        void AddBracketsString();
        Task ProcessBracketsStringAsync();
        List<string> GetBracketsRequestString();
        List<string> GetBracketsResponseString();
        
    }
}