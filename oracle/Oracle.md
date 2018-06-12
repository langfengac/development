# 数据库进阶梳理

## Oracle

### 登陆

1. 登陆cmd
`sqlplus / as sysdba`

如果这种方式登陆出现`ERROR:ORA-12560:TNS:协义适配器错误 `
改用
`sqlplus strong_fjxm/strong@orcl` 这种方式进行登陆就不会有问题。可以进入进行查询等权限操作


2. 连接
`conn `

### 权限

`grant dba,connect,resource to strong_fjxm`

### 导入导出

#### exp imp
exp

```sql
exp 
```
imp

#### 数据泵(expdp impdp)

1. expdp
```sql
expdp strong_fjxm/strong@192.168.116.201/flood directory=data_pump_dir dumpfile=FULLFJXM.DMP schemas=strong_fjxm
```

2. impdp
```sql
impdp strong_fjxm/strong@192.168.116.201/flood directory=data_pump_dir dumpfile=FULLFJXM.DMP schemas=strong_fjxm table_exists_action=replace
```

3. 如果两个用户名不一样
 `remap_schema=源库用户名:目标库用户名`
```sql
impdp strong_hn2/strong@47.95.15.215/orcl directory=data_pump_dir dumpfile=STRONG_HN220171123.DMP table_exists_action=replace remap_schema=strong_hn:strong_hn2
```

4. 如果两个命令空间不一样
`remap_tablespace=源库表空间名称:目标库表空间`
```sql
impdp strong_hn2/strong@47.95.15.215/orcl directory=data_pump_dir dumpfile=STRONG_HN220171123.DMP table_exists_action=replace remap_tablespace=strong_hn2:strong_hn2_data
```
表空间查询语句：
```sql
 select t.tablespace_name, round(sum(bytes/(1024*1024)),0) ts_size
    from dba_tablespaces t, dba_data_files d
    where t.tablespace_name = d.tablespace_name
    group by t.tablespace_name;
```

3. 权限
需要导入导出的权限
导入导出操作授权：grant exp_full_database,imp_full_database to dmuser;  (dmuser为数据库用户名)

4. 查看已经创建的路径信息：
``SELECT * FROM dba_directories;`

5. 创建路径
　　创建路径需要sys权限，需要有create any directory权限才可以创建路径。

　　选项：DIRECTORY=directory_object

　　Directory_object用于指定目录对象名称。需要注意，目录对象是使用CREATE DIRECTORY语句建立的对象，而不是OS目录。

　　eg: CREATE OR REPLACEdirectory backup_path AS 'D:\APP\ORADATA\db_backup'; --创建路径名为dackup_path的路径，并指向硬盘的指定位置

　　对新创建的路径进行授权操作：

　　eg：grant read,write on directory backup_path to orcldev; --将对路径的读写权限分配各orcldev用户。

### 跨数据库 link
```sql
create public database link dblink_fjxm
connect to strongwater identified by strongwater
using '(DESCRIPTION =

(ADDRESS_LIST =

(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.116.200)(PORT = 1521))

)

(CONNECT_DATA =

(SERVICE_NAME = orcl)

)

)';
```

1. 查询

2. 删除



### 同义词
```sql
create synonym st_stbprp_b for st_stbprp_b@dblink_fjxm;
```

``` sql 
当前用户下全有表的同义词（全库同义词）
declare
       --类型定义
       cursor c_job
       is select rownum, ' create synonym "'|| OBJECT_NAME ||'" for strong_project."' || OBJECT_NAME || '";'  sql_val  
 from all_objects where object_type  in('TABLE','VIEW','PROCEDURE','TRIGGER','FUNCTION','PACKAGE') AND  owner='STRONG_PROJECT';
       --定义一个游标变量v_cinfo c_emp%ROWTYPE ，该类型为游标c_emp中的一行数据类型
       c_row c_job%rowtype;
begin
       for c_row in c_job loop
        --DBMS_OUTPUT.put_line(c_row.rownum);
         -- DBMS_OUTPUT.put_line(c_row.sql_val);
        --  EXECUTE IMMEDIATE c_row.sql_val;  
       end loop;
end;
--查询两库中有相同表名的
WITH B AS (SELECT * FROM ALL_TABLES WHERE OWNER='STRONG_PROJECT')
select A.* from all_tables A where A.owner ='STRONG_HN'  AND EXISTS(SELECT TABLE_NAME FROM B WHERE A.TABLE_NAME=B.TABLE_NAME)
 
```



### 自定义类(TYPE)

#### object类型

```sql
create or replace type typ_warning_station as object(
 stcd char(8),
 stnm  VARCHAR2(20),
 val number(10,3),
 tm date
)
```
#### table 类型

```sql
create or replace type typ_warning_station_table as table of typ_warning_station
```

#### array 类型

#### 问题：
1.无法使用类型或表的相关性来删除或取代一个类型
原因：这个类型被其它的存储过程或函数等使用了，就被占用无法修改
解决方案：Force

``` sql
create or replace type typ_warning_station Force as object(
 stcd char(8),
 stnm  VARCHAR2(20),
 val number(10,3),
 tm date
)

```

### 触发器

### 函数

```sql
--语法如下:
create or replace function 函数名(参数1 模式 参数类型)
return 返回值类型
as
变量1 变量类型;
变量2 变量类型;
begin
    函数体;
end 函数名;
--执行
var v1 varchar2(100)
exec :v1:=function_name
```

### 存储过程

#### 参数

参数的模式有3种:(如果没有注明, 参数默认的类型为 in.)

1. in: 是参数的默认模式，这种模式就是在程序运行的时候已经具有值，在程序体中值不会改变。

2. out: 模式定义的参数只能在过程体内部赋值，表示该参数可以将某个值传递回调用他的过程

3. in out: 表示高参数可以向该过程中传递值，也可以将某个值传出去

#### 执行
 三种方式：
 1.execute

在pl/sql里面的sql windows窗体下直接执行`exec pro_1();`是会报错的。因为`exec pro_1()`被当成执行体，而执行体必须在命令窗体（command windows）进行执行，把语句当成一个整体，也就是plsql块。
所以在sql windows窗体下执行为报错
![](../read-only/oracle_procedure_01.png)

 2.call
在sql windows的执行窗体中只能用 `call pro_1()`,这样执行就是把`call pro_1()`当成一个sql语句

```sql
在命令窗口中两种方式都可以调用
exec pro_1(); --这样，相当于执行一个plsql块，即把”OUT_TIME()“看成plsql块调用。
call pro_1(); --这样，相当于，但用一个方法“OUT_TIME()”，把“OUT_TIME()”看成一个方法。
```

 3.直接执行

```sql
begin
pro_1();
end;
```

## 语法

### if
1.在oracle自定义函数中, else if 的正确写法是 elsif 而不是 else if
2.使用 if 需要加 then  "if 条件 then 操作"

```sql
create or replace function function1(para1 in number, para2 in number)
return number
as
begi
  if para1 > para2 then
      return para1;
  elsif para1 = para2 then
   	  return para2;
  else
      return para3;
  end if;
end function1;
```

### 游标
