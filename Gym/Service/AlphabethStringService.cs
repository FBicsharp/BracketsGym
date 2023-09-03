using System.Net.Http.Json;

namespace Gym.Service
{
    public class AlphabethStringService : IAlphabethStringService
	{
        private readonly ILogger<AlphabethStringService> _logger;
        private readonly HttpClient _httpClient;
		private readonly UrlBaseSettings _configurationService;

		public AlphabethStringService(ILogger<AlphabethStringService> logger, HttpClient httpClient, UrlBaseSettings configurationService)
        {
            _logger = logger;
            _httpClient = httpClient;
			_configurationService = configurationService;
		}

        public async Task<List<string>> GetAlphabethStringAsync(IEnumerable<string> inputString)
        {
            var result = new List<string>();
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_configurationService.CleaningPairsEn, inputString);
                if (response.IsSuccessStatusCode)
                {
					result = await response.Content.ReadFromJsonAsync<List<string>>();
				}
				else
                {
					_logger.LogError($"Error in {nameof(GetAlphabethStringAsync)}: {response.StatusCode}");
				}

            }
            catch (Exception ex)
            {
				_logger.LogError($"Error in {nameof(GetAlphabethStringAsync)}: {ex}");
            }
            return result;
        }


		public async Task<string> GeneratePDFAsync(IEnumerable<string> inputString)
		{
			var result = new List<string>();
			try
			{
				var response = await _httpClient.PostAsJsonAsync(_configurationService.PdfGenerator, inputString);
				if (response.IsSuccessStatusCode)
				{	
					var file = await response.Content.ReadAsStreamAsync();
					var memoryStream = new MemoryStream();
					await file.CopyToAsync(memoryStream);
					var bytes = memoryStream.ToArray();
					var base64 = Convert.ToBase64String(bytes);
					return base64;

				}
				else
				{
					_logger.LogError($"Error in {nameof(GeneratePDFAsync)}: {response.StatusCode}");
				}

			}
			catch (Exception ex)
			{
				_logger.LogError($"Error in {nameof(GeneratePDFAsync)}: {ex}");
			}
			return string.Empty;
		}


	}
}
