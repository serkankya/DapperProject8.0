using Microsoft.Data.SqlClient;
using System.Data;

namespace Project.WebAPI.Models.DapperContext
{
	public class DapperContext
	{
		readonly IConfiguration _config;
		readonly string _connectionString;

		public DapperContext(IConfiguration config)
		{
			_config = config;
			_connectionString = _config.GetConnectionString("connection");
		}

		public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
	}
}
