
using Dapper;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.StatisticsRepository
{
	public class StatisticsRepository : IStatisticsRepository
	{
		readonly DapperContext _dapperContext;

        public StatisticsRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> GetBlogCount()
		{
			string getBlogQuery = "SELECT COUNT(*) FROM Blogs WHERE Status = 1";

			using (var connection = _dapperContext.CreateConnection())
			{
				var result = await connection.ExecuteScalarAsync(getBlogQuery);
				return Convert.ToInt32(result);
			}
		}

		public async Task<int> GetReviewCount()
		{
			string getReviewQuery = "SELECT COUNT(*) FROM Reviews WHERE Status = 1";

			using (var connection = _dapperContext.CreateConnection())
			{
				var result = await connection.ExecuteScalarAsync(getReviewQuery);
				return Convert.ToInt32(result);
			}
		}

		public async Task<int> GetVehicleCount()
		{
			string getVehicleQuery = "SELECT COUNT(*) FROM Vehicles WHERE Status = 1";

			using (var connection = _dapperContext.CreateConnection())
			{
				var result = await connection.ExecuteScalarAsync(getVehicleQuery);
				return Convert.ToInt32(result);
			}
		}
	}
}
