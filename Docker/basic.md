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
## DockerFile

```cs

1. from

2. label

3. run

4. workdir
如果文件名不存在会自动创建文件，并cd到这个文件
进入一个文件尽是用mrokdir，少用run cd,以及workdir 尽量使用绝对目录

5. ADD and COPY区别
add能添加到根目录后，还带有解压的功能
大部情况下copy优于add，因为add还有额外的功能，
添加本地文件到docker的时候用add或copy，但如果是远程文件的话，可能用run curl或wget

6. ENV 声明一个常量
examples:
ENV MYSQL_VERSION 5.6
RUN agp-get install -y mysql-server="${MYSQL_VERSION}" && rm -rf /var/lib/apt/list/*

7. VOLUME and EXPOSE

8. CMD and ENTRYPOINT

```
## Shell 和 Exec 格式

## Volume

持持化方案

一、基于本地文件系统的存储持久化




二、远程第三方存储如aws NAS

提供独立与容器之外的持久化的存储。
之前我们运行容器，在`stop`


## Registry 镜像仓库
国内比较流行的镜像仓库：

1. 官方的仓库：daocloud
2. 时速云
3. aliyun

## exec 进入容器内部
docker exec -it 12i32342ew32 /bin/bash

注:docker 运行docker容器对像时，如果容器本身没什么任何事务可以做，就会自己关掉，例如：`docker run ubuntu -d` 设为后台运行了，但是你在查看docker ps -a的时候发现这个容器自动关了，所以要保持一个前台运行的，要这么做：
`docker run -d --name test001 ubuntu -c "while "`

进入docker容器后(docker exec -it test001 /bin/sh)，运行ping发现找不到这个命令，要先安装
先apt-get update 然后再apt-get install inetutils-ping

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

## 发布程序

```dos
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY . .
EXPOSE 80
ENTRYPOINT ["dotnet", "WebApplication2.dll"]
```
然后运行命令容器 `docker run --name=aspnetcoredocker1 -p 7778:80 -d  wang/core`