using Project.Shared.DTOs.BlogDtos;
using Project.Shared.DTOs.CommentDtos;

namespace Project.WebAPI.Repositories.BlogRepository
{
    public interface IBlogRepository
    {
        Task<List<ResultBlogDto>> ListActiveBlogs();
        Task<List<ResultBlogDto>> ListAllBlogs();
        Task<List<ResultBlogDto>> ListRecentBlogs(int currentBlogId);
        Task InsertBlog(InsertBlogDto insertBlogDto);
        Task UpdateBlog(UpdateBlogDto updateBlogDto);
        Task RemoveBlog(int id);
        Task<ResultBlogDto> GetBlogById(int id);
		Task<List<ResultBlogDto>> GetCommentCountAndBlogs();
        Task<List<ResultBlogDto>> SearchBlog(string keyWord);
	}
}
