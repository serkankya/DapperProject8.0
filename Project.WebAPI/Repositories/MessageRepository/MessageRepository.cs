using Dapper;
using Project.Shared.DTOs.MessageDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.MessageRepository
{
	public class MessageRepository : IMessageRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

        public MessageRepository(DapperContext dapperContext, ILogger<MessageRepository> logger)
        {
            _dapperContext = dapperContext;
			_logger = logger;	
        }

        public Task DeleteMessage(int id)
		{
			throw new NotImplementedException();
		}

		public Task GetMessageById(int id)
		{
			throw new NotImplementedException();
		}

		public async Task InsertMessage(InsertMessageDto insertMessageDto)
		{
			string insertQuery = "INSERT INTO Messages (Name, Surname, Email, Subject, Message) VALUES (@name, @surname, @email, @subject, @message)";

			var parameters = new DynamicParameters();
			parameters.Add("@name", insertMessageDto.Name);
			parameters.Add("@surname", insertMessageDto.Surname);
			parameters.Add("@email", insertMessageDto.Email);
			parameters.Add("@subject", insertMessageDto.Subject);
			parameters.Add("@message", insertMessageDto.Message);

			using (var connection = _dapperContext.CreateConnection())
			{
				await connection.ExecuteAsync(insertQuery, parameters);
			}
		}

		public Task<List<ResultMessageDto>> ListAllMessages()
		{
			throw new NotImplementedException();
		}

		public Task<List<ResultMessageDto>> ListUnreadMessages()
		{
			throw new NotImplementedException();
		}
	}
}
