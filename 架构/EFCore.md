# Entity Framework Core

官方文档：https://docs.microsoft.com/zh-cn/ef/core/get-started/install/

## Migration 迁移

坑：
执行 Add-Migration init的时间
1、如果解决方案里面有多个项目，必须保证整个解决方案生成时不会报错，要不然就就会报错的项目卸载掉 否则会一直报`Build failed.`
2、项目的启动项必须是要生成的那个项目


## 结构
https://docs.microsoft.com/zh-cn/ef/core/modeling/
#### 1. 主键：

1. 默认约定主键
主键默认是ID 或 [ClassName]ID 无大小写区分

2. 自定义主键

```cs
//通过添加特性注释来说明是主键
class Car
{
    [Key]
    public string abc { get; set; }

    public string Make { get; set; }
    public string Model { get; set; }
}


class MyContext : DbContext
{
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    //通过 Fluent API 将单个属性配置为实体的键
        modelBuilder.Entity<Blog>()
            .HasKey(c => c.Car);
    }
}
```