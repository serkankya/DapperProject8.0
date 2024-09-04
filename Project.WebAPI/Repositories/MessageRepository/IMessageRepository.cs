using Project.Shared.DTOs.MessageDtos;

namespace Project.WebAPI.Repositories.MessageRepository
{
	public interface IMessageRepository
	{
		Task<List<ResultMessageDto>> ListAllMessages();
		Task<List<ResultMessageDto>> ListUnreadMessages();
		Task InsertMessage(InsertMessageDto insertMessageDto);
		Task DeleteMessage(int id);
		Task<ResultMessageDto> GetMessageById(int id);
	}
}
