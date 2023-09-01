namespace Gym.ViewModel
{
    public class AlphabethViewModel: IAlphabethViewModel
    {
        public string CurrentString { get; set; }
        private List<string> StringsList { get; set; }
		private List<string> StringsListResponse { get; set; }
        public Action StateHasChenged { get; set; }
        public AlphabethViewModel()
        {
            StringsList = new List<string>();
			StringsListResponse = new List<string>();
			CurrentString = string.Empty;
		}

        public void AddAlphabethString()
        {
            StringsList.Add(CurrentString);
            StateHasChenged?.Invoke();
        }
        public async Task ProcessAlphabethStringAsync()
        {
            //StringsListResponse = await AlphabethStringService.GetAlphabethString(StringsList);
            StringsListResponse = StringsList;
            StateHasChenged?.Invoke();
        }
     

        public List<string> GetAlphabethRequestString() => StringsList;

        public List<string> GetAlphabethResponseString() => StringsListResponse;

        
    }
}
