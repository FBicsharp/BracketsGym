using Blazored.Toast.Services;
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
        public Action StateHasChenged { get; set; } = () => { };        
		private readonly  IAlphabethStringService _alphabethStringService;
		private readonly IToastService _toastService;
		private readonly IJSRuntime _jSRuntime;

		public AlphabethViewModel(IAlphabethStringService alphabethStringService, IToastService toastService,IJSRuntime jSRuntime)
        {
            StringsList = new List<string>();
			StringsListResponse = new List<string>();
			CurrentString = string.Empty;
			_alphabethStringService = alphabethStringService;
			_toastService = toastService;
			_jSRuntime = jSRuntime;
		}

        public async Task AddAlphabethStringAsync()
        {
            if (string.IsNullOrEmpty(CurrentString.Trim()))
                return;
            _toastService.ShowInfo($"Adding new item {CurrentString.Trim()}...");
            StringsList.Add(CurrentString);
            await ProcessAlphabethStringAsync();
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
            {
                _toastService.ShowWarning("No data to generate PDF");
                return;
            }
			StringsListResponse = await _alphabethStringService.GetAlphabethStringAsync(StringsList);
			var base64string = await _alphabethStringService.GeneratePDFAsync(StringsList);
            if (base64string.Count()==0 )
			{
				_toastService.ShowError("PDF not generated");
				return;
			}			
            _toastService.ShowSuccess("PDF generated");
            await _jSRuntime.InvokeAsync<string>("OpenPdfFile", "AlphabethStrings", base64string);
			StateHasChenged?.Invoke();

		}

        public Task RemoveStrings(int index)
        {
            _toastService.ShowInfo($"Removing item {StringsList[index]}...");
			if (StringsList.Count() > index)
				StringsList.RemoveAt(index);
			if (StringsListResponse.Count()>index)
			    StringsListResponse.RemoveAt(index);
			    StateHasChenged?.Invoke();
            return Task.CompletedTask;
		}
		public void ClearAll() 
        {
            _toastService.ShowInfo("Clearing...");
            StringsList.Clear();
            StringsListResponse.Clear();
			StateHasChenged?.Invoke();
		}
	}
}
