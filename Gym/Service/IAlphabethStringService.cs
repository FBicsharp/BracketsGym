namespace Gym.Service
{
	public interface IAlphabethStringService
	{
		Task<List<string>> GetAlphabethStringAsync(IEnumerable<string> inputString);
		Task<string> GeneratePDFAsync(IEnumerable<string> inputString);
	}
}