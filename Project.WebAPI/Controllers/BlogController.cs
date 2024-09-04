using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.BlogDtos;
using Project.WebAPI.Repositories.BlogRepository;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet("GetActiveBlogs")]
        public async Task<IActionResult> GetActiveBlogs()
        {
            var values = await _blogRepository.ListActiveBlogs();
            return Ok(values);
        }

        [HttpGet("GetAllBlogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var values = await _blogRepository.ListAllBlogs();
            return Ok(values);
        }

        [HttpPost("InsertBlog")]
        public async Task<IActionResult> InsertBlog(InsertBlogDto insertBlogDto)
        {
            await _blogRepository.InsertBlog(insertBlogDto);
            return Ok("Blog inserted successfully.");
        }

        [HttpPut("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
        {
            await _blogRepository.UpdateBlog(updateBlogDto);
            return Ok("Blog updated successfully.");
        }

        [HttpPut("RemoveBlog")]
        public async Task<IActionResult> RemoveBlog(int id)
        {
            await _blogRepository.RemoveBlog(id);
            return Ok("Blog removed successfully.");
        }

        [HttpGet("GetBlog/{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var values = await _blogRepository.GetBlogById(id);
            return Ok(values);
        }
    }
}
