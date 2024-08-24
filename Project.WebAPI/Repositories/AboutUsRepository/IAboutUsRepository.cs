using Project.Shared.DTOs.AboutUsDtos;

namespace Project.WebAPI.Repositories.AboutUsRepository
{
	public interface IAboutUsRepository
	{
		Task<ResultAboutUsDto> GetAboutUs();
		Task UpdateAboutUs(UpdateAboutUsDto updateAboutUsDto);
	}
}
