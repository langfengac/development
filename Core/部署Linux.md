# 部署Centos

注：**centos环境**可以关注[**linux篇**](./linux.md)

## 一.环境安装(net core)
到微软官方下载https://www.microsoft.com/net/download/
选择linux>centos进行安装core环境  地址：https://www.microsoft.com/net/download/linux-package-manager/centos/sdk-current

1. 在安装dotnet之前，您需要注册Microsoft密钥、注册产品存储库并安装所需的依赖项。每台机器只需要做一次
`sudo rpm -Uvh https://packages.microsoft.com/config/rhel/7/packages-microsoft-prod.rpm`

2. 更新供安装的产品，安装core SDK
`sudo yum update`
`sudo yum install dotnet-sdk-2.1`

## 二.程序发布(net core)

3. 发布到linux，使用ftp
选择连接类型为`SFTP`,放到`/home/mycore/`目录下
![下载](..\image\core8.png)
![下载](..\image\core9.png)

4. 上传成功后，通用 `cd` 进入网站根目录 `/home/mycore/` 运行网站程序
```js
//命令：
dotnet WebApplication1.dll
//如果在任意非站点根目录，通过下面这种方式直接运行，程序会抛异常，不知是程序原因还是其他原因。
dotnet /home/mycore/WebApplication1.dll
```
![下载](..\image\core10.png)

5. 运行 `curl http://localhost:5000`,就可以看到访问的代码结果，说明发布成功

## 三.反向代理，浏览器访问服务器web服务

3. 接下来，让外网浏览器可以访问服务器的地址，修改nginx nginx.conf 进行代理
首先，拿到Nginx的默认配置文件/etc/nginx/nginx.conf，把默认80端口转发配置server节点用#符注释掉。
![下载](..\image\core5.png)
然后在`/etc/nginx/conf.d/`目录下新建*.conf的后缀的文件，内容如下
```js
server {
    listen 80;
    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
```

4. 执行 `nginx -s reload` 或 `systemctl restart nginx ` 重启nginx。就可以访问了

5. 在本地浏览器访问服务器地址的时候，运行可能会出现如下情况
![下载](..\image\core6.png)
这个问题是由于`SELinux`保护机制所导致，我们需要将Nginx添加至`SELinux`的白名单
```js
yum install policycoreutils-python
sudo cat /var/log/audit/audit.log | grep nginx | grep denied | audit2allow -M mynginx
sudo semodule -i mynginx.pp
```

6. 再次访问就可以了
![下载](..\image\core7.png)

## 四.配置守护进程 Supervisor
**Supervisor**是用Python开发的Linux/Unix系统下的一个进程管理工具。它可以使进程脱离终端，变为后台守护进程（daemon）。实时监控进程状态，异常退出时能自动重启。

**Supervisor**不支持任何版本的Window系统；仅支持在Python2.4或更高版本，但不能在任何版本的Python 3下工作。

其主要组成部分：

**supervisord**：Supervisor的守护进程服务，用于接收进程管理命令；

**supervisorctl**：Supervisor命令行工具，用于和守护进程通信，发送管理进程的指令；

**Web Server**：Web端进程管理工具，提供与supervisorctl类似功能，管理进程；

**XML-RPC Interface**：提供XML-RPC接口，请参阅XML-RPC API文档。

使用方法：待补https://www.cnblogs.com/esofar/p/8043792.html

