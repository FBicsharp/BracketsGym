namespace Gym.ViewModel
{
    public class BracketsViewModel : IBracketsViewModel
	{
        public string CurrentString { get; set; }
        private List<string> StringsList { get; set; }
		private List<string> StringsListResponse { get; set; }
        public Action StateHasChenged { get; set; }
        public BracketsViewModel()
        {
            StringsList = new List<string>();
			StringsListResponse = new List<string>();
			CurrentString = string.Empty;
		}

        public void AddBracketsString()
        {
            StringsList.Add(CurrentString);
            StateHasChenged?.Invoke();
        }
        public async Task ProcessBracketsStringAsync()
        {
            //StringsListResponse = await BracketsStringService.GetBracketsString(StringsList);
            StringsListResponse = StringsList;
            StateHasChenged?.Invoke();
        }
     

        public List<string> GetBracketsRequestString() => StringsList;

        public List<string> GetBracketsResponseString() => StringsListResponse;

        
    }
}
