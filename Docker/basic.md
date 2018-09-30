# Docker 基础

## 术语

1. host 宿主机以
2. image 镜像
3. container 容器
4. registry 仓库
5. daemon 守护程序
6. client 客户端


## 命令

``` dos
 docker pull

 docker build
 docker images
 docker ps
 cocker run

 docker rm
 dpcker rmi 删除镜像
 //有时候会遇到遇一个镜像多个tag的时候，无法删除，有种解决方案

 1. -f 强制删除多个
 docker rmi -f 4994f1f799c7

 2. 只删除某个tag 语法：docker rmi repository:tag
 docker rmi mysql:8.10

 docker cp
 docker commit


```

``` dos
**docker ps -a** 可以查看所有实例，包含已停止未删的实例
docker stop 停止运行容器
docker rm container_id (container_id可以通过 docker ps -a查看，例如3e1b106c25f3)
```

## Volume

提供独立与容器之外的持久化的存储。
之前我们运行容器，在`stop`


## Registry 镜像仓库
国内比较流行的镜像仓库：

1. 官方的仓库：daocloud
2. 时速云
3. aliyun



## mysql 镜像引入 并开启远程

1. ysql 界面管理工具选用 Navicat Premium

2. 镜像下载：查找可以上hub.docker.com 或者docker search mysql来进行查询

3. 拉取镜像：git pull [名称加tag] 最好上hub.docker上看一下版本，否则是下载最新的版本latest

4. 运行镜像：实例 命令：`docker run --name some-mysql -e MYSQL_ROOT_PASSWORD=my-secret-pw -d mysql`
说明：some-mysql 为容器name,my-secret-pv 为密码，记得加个-p 映射端口，以便外部连接调用,mysql镜像的默认内部端口为3306
例如：
docker run --name testmysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=123456 -d mysql

5. 运行完成，进入docker/mysql容器，命令 docker exec -it testmysql bash

6. 测试mysql：进入mysql,命令 mysql -u root -p;

7. 测试mysql：成功进来，可以查看一下数据库show databases;成功，说明运行正常

8. 外部链接mysql:运行Navicat Premium 连接正常
如果报错 `Client does not support authentication protocol requested by server; consider upgrading MySQL client`
一般由于远程设置没弄好，要进入docker=>mysql里去设置用户允许远程登陆
方法：

```dos
docker exec -it testmysql bash
mysql -u root -p;
备注 ： mysql -u 最高权限用户名 -p   再输入密码进入
(1)查看用户配置项
select host,user,plugin,authentication_string from mysql.user;
备注：host为 % 表示不限制ip   localhost表示本机使用    plugin非mysql_native_password 则需要修改密码
(2)修改用户密码
这里我们执行一下
ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY '12345';//重置一下就可以远程了
备注：
1.如果想要设置远程这样设置：ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'newpassword'; #更新一下用户的密码 root用户密码为newpassword

2.如果要设置本地plugin为mysql_native_password这样设置：

ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'newpassword'; #更新一下用户的密码 root用户密码为newpassword

flush privileges;
```

