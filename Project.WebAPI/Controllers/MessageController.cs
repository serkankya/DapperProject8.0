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

		[HttpPost("InsertMessage")]
		public async Task<IActionResult> InsertMessage(InsertMessageDto insertMessageDto)
		{
			await _messageRepository.InsertMessage(insertMessageDto);
			return Ok("Message inserted successfully.");
		}
	}
}
