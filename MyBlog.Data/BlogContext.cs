using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models;

namespace MyBlog.Data
{
    public class BlogContext:IdentityDbContext<User>
    {
        private DbContextOptions<BlogContext> options;

        public BlogContext()
        {

        }

        public BlogContext(DbContextOptions<BlogContext> options)
            :base(options)
        {
            this.options = options;
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Data Source=.\\SQLEXPRESS;Database=MyBlog;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }
    }
}
