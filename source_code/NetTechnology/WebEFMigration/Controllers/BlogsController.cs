using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebEFMigration.Model;

namespace WebEFMigration.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BloggingContext _context;

        public BlogsController(BloggingContext context)
        {
            _context = context;
        }
        [Route("Blogs/index")]
        public IActionResult Index()
        {
            //var blogs = _context.Blogs.FirstOrDefault() ;
            //ef操作

            //blogs.Posts.Add(new Post() { Content = "888888888888", Title = "1111" });
            //blogs.Posts.Add(new Post() { Content = "999999999", Title = "1111" });
            //blogs.Posts.Add(new Post() { Content = "6666666666", Title = "1111" });


            try
            {
                var blog = new Blog(_context);
                blog._validatedUrl = "1";
                blog.LastModified = DateTime.Now;

                _context.Blogs.Attach(blog);
                _context.Entry(blog).Property(md => md.LastModified).IsModified = true;


                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
           

           

            // _context.Add(b);

            //获取ID




            return View();
        }
    }
}