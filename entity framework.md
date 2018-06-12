# Entity Framework

## 关联表 Join

对于ADO.NET 中的表连接，以前一直是采用建视图的方式，但绝对太麻烦，而且映射可能会影响到速度，因此可以采用linq的方式。
linq方式 
``` sql
     (from stock in dbContext.Property_Info
                          join proType in dbContext.Property_Type
                             on stock.PropertyType equals proType.PropertyType_ID
                         select new
                         {
                             stock.CreateUserID,
                             stock.DeptID,
                             stock.HighestStock,
                             stock.LowestStock,
                             proType.Name,
                         })
```
select new 结果是只读的，
左连接：
``` sql
 var query = (from stock in dbContext.Property_Info
                          join proType in dbContext.Property_Type
                             on stock.PropertyType equals proType.PropertyType_ID
                             into joinStockType
                         from proType in joinStockType.DefaultIfEmpty()
                         select new
                         {
                             stock.CreateUserID,
                             stock.DeptID,
                             stock.HighestStock,
                             stock.LowestStock,
                             proType.Model,
                         })
```
右连接：左连接表位置互换

## 加载方式 Loading


### 懒加载  Lazy Loading
Lazy Loading使用的是动态代理，关闭Lazy Loading，可以将LazyLoadingEnabled设为false，如果导航属性没有标记为virtual，Lazy Loading也是不起作用的。

### 预加载 Eager Loading

使用Include方法关联预先加载的实体。

### 显示加载 Explicit Loading

使用Entry方法，对于集合使用Collection，单个实体则使用Reference。