using Project.Shared.DTOs.TestimonialDtos;

namespace Project.WebAPI.Repositories.TestimonialRepository
{
	public interface ITestimonialRepository
	{
		Task<List<ResultTestimonialDto>> ListActiveTestimonials();
		Task<List<ResultTestimonialDto>> ListAllTestimonials();
		Task<ResultTestimonialDto> GetTestimonialById(int id);
		Task InsertTestimonial(InsertTestimonialDto insertTestimonialDto);
		Task UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto);
		Task RemoveTestimonial(int id);
	}
}
