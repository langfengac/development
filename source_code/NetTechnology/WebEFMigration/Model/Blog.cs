using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebEFMigration.Model
{
    public class Blog
    {
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NameID { get; set; }

        public string _validatedUrl;

        public DateTime? LastModified { get; set; }
        public Blog(BloggingContext bloggingContext)
        {

        }

        public string Url
        {
            get { return _validatedUrl; }
        }
         

        public DateTime? CreateTime { get; set; }

        public List<Post> Posts { get; set; }
      
    }
   [NotMapped]
    public class BlogMetadata
    {
        public DateTime LoadedFromDatabase { get; set; }
    }
    public class Post
    {
       
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogID { get; set; }
    }


    public class Car
    {
        public int CarId { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public List<RecordOfSale> SaleHistory { get; set; }
    }

    public class RecordOfSale
    {
        public int RecordOfSaleId { get; set; }
        public DateTime DateSold { get; set; }
        public decimal Price { get; set; }

        public string CarLicensePlate { get; set; }
        public Car Car { get; set; }
    }

    public class AuditEntry
    {
        public int AuditEntryId { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
    }
    public class BloggingContext : DbContext
    {
       
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }


        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //modelBuilder.Entity<AuditEntry>();
            //modelBuilder.Entity<Blog>()
            //    .HasKey(c => c.NameID);

            builder.Entity<RecordOfSale>()
           .HasOne(s => s.Car)
           .WithMany(c => c.SaleHistory)
           .HasForeignKey(s => s.CarLicensePlate)
           .HasPrincipalKey(c => c.LicensePlate);

            builder.Entity<Blog>()
           .Property(b => b.Url)
           .HasField("_validatedUrl");//指定字段，就可以不用属性

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=127.0.0.1; Port=3306; Database=WebBloggingDB; Uid=root; Pwd=123456;");
        //}
    }
}
