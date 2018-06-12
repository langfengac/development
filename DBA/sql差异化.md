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