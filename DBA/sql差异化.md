# SQL异差化

## 如果不存在 显示另一个值[isnull nvl nvl2 decode]

一、MSSQLSERVER：isnull
Example:

```sql
ISNULL ( check_expression , replacement_value )
select isnull(value,'111')
```

二、Oracle:

nvl

``` sql
nvl(expr1,expr2)

Example:
select nvl(value,'aaa') from dual;
```

nvl2

```sql
nvl2(expr1,expr2,expr3)

Example:
select nvl(vlaue,'value不为空','value为空') from dual;
```

decode

```sql
decode(条件,值1,返回值1,值2,返回值2,...值n,返回值n,缺省值)

Example:
select id, username, age, decode(sex,0,'男',1,'女') from users;
```

## 联接 left join |right join | full [out] join | inner join | cross join | union |union all

1. demo 数据
![](..\image\sql_01.png)

### Inner join

``` sql
--Inner Join
--筛选两边都有的记录
SELECT *
FROM ORDERS o INNER JOIN CONSUMERS c
ON o.CONSUMER_ID = c.CONSUMER_ID
```
结果：
![](..\image\sql_02.png)

### Left join

```sql
--Left Join
--以左边的表为主表，列出主表所有记录，匹配能匹配的，不能匹配的用NULL列出
SELECT * 
FROM CONSUMERS c left  join ORDERS o
on c .CONSUMER_ID = o .CONSUMER_ID

```
结果
![](..\image\sql_03.png)

### Right join
以右边的表为主表，列出主表所有记录，匹配能匹配的，不能匹配的用NULL列出

### Full [out] join

```sql
--Full Out Join
--两边都筛选出来，匹配能匹配的，不能匹配的用NULL列出
SELECT *
FROM ORDERS o FULL JOIN CONSUMERS c
ON o.CONSUMER_ID = c.CONSUMER_ID

--注：以上Sqlserver 可以加out或不加out 但是oracle 不能加out
```
结果：
![](..\image\sql_04.png)

### Cross Join

``` sql
--Cross Join
--列出两边所有组合，即笛卡尔集A×B
SELECT *
FROM ORDERS o CROSS JOIN CONSUMERS c
--注：Oracle不支持
```
结果：
![](..\image\sql_05.png)

### Union 与 Union All

UNION 操作符用于合并两个或多个 SELECT 语句的结果集。
请注意，UNION 内部的 SELECT 语句必须拥有相同数量的列。列也必须拥有相似的数据类型。同时，每条 SELECT 语句中的列的顺序必须相同。UNION 只选取记录，而UNION ALL会列出所有记录。



## with as

### 批量插入时

```sql
--Oracle
--Oracle With as 写在Insert into 后面
--例句
Insert Into table1(value1,value2,value3)
with st as
(
select value1,value2,value4 from table2
)
select * from st;

--Sqlserver
--Sqlserver With as 写在最前面
--例子
with st as
(
select value1,value2,value4 from table2
)
Insert Into table1(value1,value2,value3)
select * from st;
```
## top
```sql
--Oracle
--Oracle没有top，改用为RowNum来处理这种需种
select * from  table1 where rownum<10 order by id desc;

--Sqlserver
select top 10 * from table1 order by id desc;
--关于top在sqlserver的性能影响，请关注性能篇top
```
[top性能](https://www.cnblogs.com/lykbk/p/rgrtrrtrtrt34343434343.html)

## GUID
```sql
--Oracle
select sys_guid() from dual;
--Sqlserver
select newid()
```