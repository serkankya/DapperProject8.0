using Project.Shared.DTOs.BlogDtos;

namespace Project.WebAPI.Repositories.BlogRepository
{
    public interface IBlogRepository
    {
        Task<List<ResultBlogDto>> ListActiveBlogs();
        Task<List<ResultBlogDto>> ListAllBlogs();
        Task<List<ResultBlogDto>> ListRecentBlogs();
        Task InsertBlog(InsertBlogDto insertBlogDto);
        Task UpdateBlog(UpdateBlogDto updateBlogDto);
        Task RemoveBlog(int id);
        Task<ResultBlogDto> GetBlogById(int id);
    }
}
