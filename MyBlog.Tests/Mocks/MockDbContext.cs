using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using System;

namespace MyBlog.Tests.Mocks
{
    public class MockDbContext
    {
        public BlogContext GetContext()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .Options;
            
            return new BlogContext(options);
        }
    }
}
