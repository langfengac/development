using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFMigration
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }


    public class BloggingContext : DbContext
    {
        //public BloggingContext(DbContextOptions<BloggingContext> options)
        //  : base(options)
        //{ }

       
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //配置mariadb连接字符串
            optionsBuilder.UseMySql("Server=127.0.0.1;Port=3306;Database=BloggingDB; User=root;Password=123456;");
        }
    }
}
