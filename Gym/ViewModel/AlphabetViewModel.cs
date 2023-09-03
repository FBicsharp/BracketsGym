using Gym.Service;
using Microsoft.JSInterop;
using System;

namespace Gym.ViewModel
{
    public class AlphabethViewModel: IAlphabethViewModel
    {
        public string CurrentString { get; set; }
        private List<string> StringsList { get; set; }
		private List<string> StringsListResponse { get; set; }
        public Action StateHasChenged { get; set; }
        public IJSRuntime JS { get; set; }
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
            ProcessAlphabethStringAsync();
			StateHasChenged?.Invoke();
        }
        public async Task ProcessAlphabethStringAsync()
        {
            StringsListResponse = await _alphabethStringService.GetAlphabethStringAsync(StringsList);            
            StateHasChenged?.Invoke();
        }
     

        public List<string> GetAlphabethRequestString() => StringsList;

        public List<string> GetAlphabethResponseString() => StringsListResponse;

		public async Task GeneratePDFAsync()
		{
            if (StringsList.Count==0)
                return;
			StringsListResponse = await _alphabethStringService.GetAlphabethStringAsync(StringsList);
			var base64string = await _alphabethStringService.GeneratePDFAsync(StringsList);
            if (base64string.Count()==0 )
                return;
            await JS.InvokeAsync<string>("OpenPdfFile", "AlphabethStrings", base64string);
			StateHasChenged?.Invoke();

		}

        public Task RemoveStrings(int index)
        {
            
			if (StringsList.Count() > index)
				StringsList.RemoveAt(index);
			if (StringsListResponse.Count()>index)
			    StringsListResponse.RemoveAt(index);
			    StateHasChenged?.Invoke();
            return Task.CompletedTask;
		}
		public void ClearAll() 
        {
            StringsList.Clear();
            StringsListResponse.Clear();
			StateHasChenged?.Invoke();
		}
	}
}
