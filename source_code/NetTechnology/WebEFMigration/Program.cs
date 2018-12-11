using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Polly;
using WebEFMigration.Model;

namespace WebEFMigration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using (var db = new BloggingContext())
            //{
            //    //检查迁移
            //    CheckMigrations(db);

            //}
            CreateWebHostBuilder(args).Build().Run();

        }
        /// <summary>
        /// 检查迁移
        /// </summary>
        /// <param name="db"></param>
        static void CheckMigrations(BloggingContext db)
        {
            Console.WriteLine("Check Migrations");

            //判断是否有待迁移
            if (db.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Migrating...");
                //执行迁移
                db.Database.Migrate();
                Console.WriteLine("Migrated");
            }
            Console.WriteLine("Check Migrations Coomplete!");
        }

        static void DoMigrations()
        {
            Console.WriteLine("Entity Framework Core Migrate Start !");
            Console.WriteLine("Get Pending Migrations...");

            using (var db = new BloggingContext(null))
            {
                //获取所有待迁移
                Console.WriteLine($"Pending Migrations：\n{string.Join('\n', db.Database.GetPendingMigrations().ToArray())}");

                Console.WriteLine("Do you want to continue?(Y/N)");

                if (Console.ReadLine().Trim().ToLower() == "n")
                {
                    return;
                }

                Console.WriteLine("Migrating...");

                try
                {

                    //执行迁移
                    db.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            Console.WriteLine("Entity Framework Core Migrate Complete !");
            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            ;
    }

    /// <summary>
    /// IWebHost 扩展方法
    /// </summary>
    public static class WebHostExtensions
    {
        /// <summary>
        /// 针对Efcore数据库迁移
        /// </summary>
        /// <typeparam name="TContext">需要作迁移的DbContext</typeparam>
        /// <param name="webHost">IWebHost</param>
        /// <param name="seeder"></param>
        /// <returns></returns>
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost,
            Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    logger.LogInformation($"准备执行数据库迁移 {typeof(TContext).Name}");
                    // 利用Policy进行重试
                    var retry = Policy.Handle<MySqlException>()
                        .WaitAndRetry(new[]
                        {
                            TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(8),
                        });

                    retry.Execute(() =>
                    {
                        // 先执行迁移再调用添加数据方法
                        context.Database.Migrate();
                        seeder(context, services);
                    });

                    logger.LogInformation($"数据库迁移完成 {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"数据库迁移发生错误 {typeof(TContext).Name}");
                }
            }

            return webHost;
        }
    }
}
