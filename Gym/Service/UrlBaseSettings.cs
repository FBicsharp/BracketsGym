using System.Net.Http.Json;

namespace Gym.Service
{
    public class UrlBaseSettings 
    {
        public int SecondTimeout { get; set; }
        public string BaseAddress { get; set; }= "http://localhost:33500/";
        public string CleaningBrackets { get; set; } = "cleanbrackets";

		public string CleaningPairsEn { get; set; }= "cleanpairs-en";
        public string PdfGenerator { get; set; } = "topdf";
		


	}
}
