namespace Project.WebAPI.Repositories.StatisticsRepository
{
	public interface IStatisticsRepository
	{
		Task<int> GetVehicleCount();
		Task<int> GetBlogCount();
		Task<int> GetReviewCount();
	}
}
