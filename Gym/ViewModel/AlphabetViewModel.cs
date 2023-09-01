using Gym.Service;

namespace Gym.ViewModel
{
    public class AlphabethViewModel: IAlphabethViewModel
    {
        public string CurrentString { get; set; }
        private List<string> StringsList { get; set; }
		private List<string> StringsListResponse { get; set; }
        public Action StateHasChenged { get; set; }
		private readonly  IAlphabethStringService _alphabethStringService;

		public AlphabethViewModel(IAlphabethStringService alphabethStringService)
        {
            StringsList = new List<string>();
			StringsListResponse = new List<string>();
			CurrentString = string.Empty;
			_alphabethStringService = alphabethStringService;
		}

        public void AddAlphabethString()
        {
            StringsList.Add(CurrentString);
            StateHasChenged?.Invoke();
        }
        public async Task ProcessAlphabethStringAsync()
        {
            StringsListResponse = await _alphabethStringService.GetAlphabethStringAsync(StringsList);            
            StateHasChenged?.Invoke();
        }
     

        public List<string> GetAlphabethRequestString() => StringsList;

        public List<string> GetAlphabethResponseString() => StringsListResponse;

        
    }
}
