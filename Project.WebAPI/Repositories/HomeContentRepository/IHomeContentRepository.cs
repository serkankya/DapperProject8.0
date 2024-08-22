using Project.Shared.DTOs.HomeContentDtos;

namespace Project.WebAPI.Repositories.HomeContentRepository
{
	public interface IHomeContentRepository
	{
		Task<ResultHomeContentDto> GetHomeContent();
		Task UpdateHomeContent(UpdateHomeContentDto updateHomeContentDto);
	}
}
