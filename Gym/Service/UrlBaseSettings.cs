using System.Net.Http.Json;

namespace Gym.Service
{
    public class UrlBaseSettings 
    {
        public int SecondTimeout { get; set; }
        public string BaseAddress { get; set; }
		public string CleaningBrackets { get; set; }
        public string CleaningPairsEn { get; set; }
        public string PdfGenerator { get; set; }
		


	}
}
