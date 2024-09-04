using Dapper;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
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

        public async Task DeleteMessage(int id)
        {
            string deleteQuery = "DELETE FROM Messages WHERE MessageId = @messageId";

            var parameters = new DynamicParameters();
            parameters.Add("@messageId", id);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    await connection.ExecuteAsync(deleteQuery, parameters);
                }
                _logger.LogInformation("Message deleted successfully: " + id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " An error occured while deleting the message: " + id);
                throw;
            }
        }

        public async Task<ResultMessageDto> GetMessageById(int id)
        {
            var getQuery = "SELECT * FROM Messages WHERE MessageId = @messageId";

            var parameters = new DynamicParameters();
            parameters.Add("@messageId", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultMessageDto>(getQuery, parameters);

                if (values == null)
                {
                    _logger.LogError("Message not found. " + id);

                    return new ResultMessageDto
                    {
                        MessageId = 0,
                        Subject = "Message not found!"
                    };
                }

                return values;
            }
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

        public async Task<List<ResultMessageDto>> ListAllMessages()
        {
            string listQuery = "SELECT * FROM Messages";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultMessageDto>(listQuery);
                return values.ToList();
            }
        }

        public async Task<List<ResultMessageDto>> ListUnreadMessages()
        {
            string listUnreadsQuery = "SELECT * FROM Messages WHERE IsRead = 0";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultMessageDto>(listUnreadsQuery);
                return values.ToList();
            }
        }
    }
}
