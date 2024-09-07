using Project.Shared.DTOs.Reviews;

namespace Project.WebAPI.Repositories.ReviewRepository
{
	public interface IReviewRepository
	{
		Task<List<ResultReviewDto>> ListRelatedReviews(int id);
		Task<List<ResultReviewDto>> ListActiveReviews();
		Task<List<ResultReviewDto>> ListAllReviews();
		Task InsertReview(InsertReviewDto insertReviewDto);
		Task UpdateReview(UpdateReviewDto updateReviewDto);
		Task RemoveReview(int id);
		Task<ResultReviewDto> GetReviewById(int id);
	}
}
