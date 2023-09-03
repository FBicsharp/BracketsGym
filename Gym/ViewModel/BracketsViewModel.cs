using Gym.Service;

namespace Gym.ViewModel
{
    public class BracketsViewModel : IBracketsViewModel
	{
		private readonly IBracketsStringService _bracketsStringService;
        public string CurrentString { get; set; }

		private List<string> StringsList { get; set; }
		private List<string> StringsListResponse { get; set; }
        public Action StateHasChenged { get; set; }
        public BracketsViewModel(IBracketsStringService bracketsStringService)
        {
            StringsList = new List<string>();
			StringsListResponse = new List<string>();
			CurrentString = string.Empty;
            _bracketsStringService = bracketsStringService;
		}

        public async Task AddBracketsStringAsync()
        {
            StringsList.Add(CurrentString);
            await ProcessBracketsStringAsync();
			StateHasChenged?.Invoke();
        }
        public async Task ProcessBracketsStringAsync()
        {
            StringsListResponse = await _bracketsStringService.GetBracketsStringAsync(StringsList);            
            StateHasChenged?.Invoke();
        }
		public void ClearAll()
		{
			StringsList.Clear();
			StringsListResponse.Clear();
			StateHasChenged?.Invoke();
		}


		public List<string> GetBracketsRequestString() => StringsList;

        public List<string> GetBracketsResponseString() => StringsListResponse;

        
    }
}
