using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.MessageDtos;
using Project.WebAPI.Repositories.MessageRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MessageController : ControllerBase
	{
		readonly IMessageRepository _messageRepository;

		public MessageController(IMessageRepository messageRepository)
		{
			_messageRepository = messageRepository;
		}

		[HttpGet("ListAllMessages")]
		public async Task<IActionResult> GetAllMessages()
		{
			var values = await _messageRepository.ListAllMessages();
			return Ok(values);
		}

		[HttpGet("ListUnreadMessages")]
		public async Task<IActionResult> GetUnreadMessages()
		{
			var values = await _messageRepository.ListUnreadMessages();
			return Ok(values);
		}

		[HttpPost("InsertMessage")]
		public async Task<IActionResult> InsertMessage(InsertMessageDto insertMessageDto)
		{
			await _messageRepository.InsertMessage(insertMessageDto);
			return Ok("Message inserted successfully.");
		}

		[HttpGet("DeleteMessage/{id}")]
		public async Task<IActionResult> DeleteMessage(int id)
		{
			await _messageRepository.DeleteMessage(id);
			return Ok("Message deleted successfully.");
		}

		[HttpGet("GetMessage")]
		public async Task<IActionResult> GetMessageById(int id)
		{
			var values = await _messageRepository.GetMessageById(id);
			return Ok(values);
		}
	}
}
