using Blog.API.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        [HttpGet]
        public IEnumerable<Models.Blog> Get()
        {
            return [.. _appDbContext.Blogs];
        }

        [HttpGet("{id}")]
        public async Task<List<Models.Blog>> Get(int id)
        {
            return [.. _appDbContext.Blogs];
        }

        [HttpPost]
        public async Task<Models.Blog> Post([FromBody] Models.Blog product)
        {
            var result = await _appDbContext.Blogs.AddAsync(product);

            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        [HttpPut("{id}")]
        public async Task<Models.Blog> Put(int id, [FromBody] Models.Blog product)
        {
            var pr = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            pr.Name = product.Name;
            pr.Description = product.Description;
            var result = _appDbContext.Blogs.Update(pr).Entity;
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Models.Blog> Delete(int id)
        {
            var pr = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            var result = _appDbContext.Blogs.Remove(pr).Entity;

            await _appDbContext.SaveChangesAsync();

            return result;
        }
    }
}
