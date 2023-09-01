namespace Gym.Service
{
	public interface IBracketsStringService
	{
		Task<List<string>> GetBracketsStringAsync(List<string> inputString);
	}
}