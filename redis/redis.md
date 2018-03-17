# 数据库进阶梳理

## Redis

1. Redis 是一个高性能的key-value数据库。
2. 它支持存储的value类型相对更多，包括string(字符串)、list(链表)、set(集合)、zset(sorted set --有序集合)和hash（哈希类型）。
3. 周期性的把更新的数据写入磁盘或者把修改操作写入追加的记录文件

### 使用



### 安装

1. windows

下载地址：https://github.com/MSOpenTech/redis/releases
Redis 支持 32 位和 64 位。这个需要根据你系统平台的实际情况选择，这里我们下载 Redis-x64-xxx.zip压缩包到 C 盘，解压后，将文件夹重新命名为 redis。
![下载](..\image\redis_01.jpg)

Redis 由四个可执行文件：`redis-benchmark、redis-cli、redis-server、redis-stat `这四个文件，加上一个redis.conf就构成了整个redis的最终可用包。它们的作用如下：
````
redis-server：Redis服务器的daemon启动程序
redis-cli：Redis命令行操作工具。当然，你也可以用telnet根据其纯文本协议来操作
redis-benchmark：Redis性能测试工具，测试Redis在你的系统及你的配置下的读写性能
redis-stat：Redis状态检测工具，可以检测Redis当前状态参数及延迟状况
````

打开一个 cmd 窗口 使用cd命令切换目录到 C:\redis 运行 redis-server.exe redis.windows.conf 。
如果想方便的话，可以把 redis 的路径加到系统的环境变量里，这样就省得再输路径了，后面的那个 redis.windows.conf 可以省略，如果省略，会启用默认的。输入之后，会显示如下界面：
![](..\image\redis_02.png)

这时候另启一个cmd窗口，原来的不要关闭，不然就无法访问服务端了。
切换到redis目录下运行 redis-cli.exe -h 127.0.0.1 -p 6379 。
设置键值对 set myKey abc
取出键值对 get myKey
![](..\image\redis_02.jpg)

#### 部署windows服务

1. 按住 Shift键+ 鼠标右键 如下图：
![](..\image\redis1.png)

2. 安装 Redis服务 设置端口 为6666

````
安装服务： redis-server --service-install --service-name redisService6666 --port 6666

启动进程： redis-server --service-start --service-name redisService6666

停止进程： redis-server --service-stop --service-name redisService6666
````
![](..\image\redis2.png)

3. 查看 调出任务管理器
![](..\image\redis3.png)

4. 连接 Redis服务(这个客户端工具就是Redis Desktop Manager)
![](..\image\redis4.png)

5. 使用 redis-cli.exe 连接redis 服务
连接指令：redis-cli.exe  -h 127.0.0.1 -p 6666
![](..\image\redis5.png)

6. 卸载 Redis 服务
停止进程： redis-server --service-stop --service-name redisService6666

卸载命令：sc delete redisService6666
![](..\image\redis6.png)

1. linux

下载地址：http://redis.io/download，下载最新文档版本。
本教程使用的最新文档版本为 2.8.17，下载并安装：

``` dos
$ wget http://download.redis.io/releases/redis-2.8.17.tar.gz
$ tar xzf redis-2.8.17.tar.gz
$ cd redis-2.8.17
$ make

```

make完后 redis-2.8.17目录下会出现编译后的redis服务程序redis-server,还有用于测试的客户端程序redis-cli,两个程序位于安装目录 src 目录下：
下面启动redis服务.

``` dos
$ cd src
$ ./redis-server

```

注意这种方式启动redis 使用的是默认配置。也可以通过启动参数告诉redis使用指定配置文件使用下面命令启动。

``` dos
$ cd src
$ ./redis-server redis.conf
```


redis.conf是一个默认的配置文件。我们可以根据需要使用自己的配置文件。
启动redis服务进程后，就可以使用测试客户端程序redis-cli和redis服务交互了。 比如：

``` dos
$ cd src
$ ./redis-cli
redis> set foo bar
OK
redis> get foo
"bar"
```

3. Ubuntu

### 配置

#### 设置密码

运行`redis-cli.exe -h 127.0.0.1 -p 6379`
```` dos
 指令：

1.设置密码： config set requirepass 123456

2.查看：info（验证无法通过）

3.授权登陆  auth 123456

````
![](..\image\redis7.png)

#### 修改端口


修改redis.conf文件，找到port 6379,修改成自己想要的接口
如果你直接双击行运redis-server.exe发现运行的端口还是6379
你要在cmd里面行运redis-server.exe redis.conf 这样子就会根据你改的配置文件进行运行

### 功能使用

#### 主从配置

#### 订阅发布

#### 集群

#### 事务

#### 分布式锁

#### 可视化工具

1. Redis Desktop Manager (推荐)
几款开源的图形化Redis客户端管理软件推荐
支持: Windows 7+, Mac OS X 10.10+, Ubuntu 14+
特点： C++ 编写，响应迅速，性能好。但不支持数据库备份与恢复。
项目地址： https://github.com/uglide/RedisDesktopManager

1. Redis Client
项目简介：　使用Java编写，功能丰富，缺点是性能稍差，网络不好时，会不时断线。
项目地址：　https://github.com/caoxinyu/RedisClient

1. Redis Studio
项目简介：　又一个C++编写的redis管理工具，仅支持windows平台，支持xp操作系统。
项目地址： https://github.com/cinience/RedisStudio
ps: 后面两款为国人开发。