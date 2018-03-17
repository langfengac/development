# NVM

Node版本管理器--nvm，可以运行在多种操作系统上。nvm for windows 是使用go语言编写的软件。 我电脑使用的是Windows操作系统，所以我要记录下在此操作系统上nvm的安装和使用。

## 下载

nvm-windows 最新下载地址：
https://github.com/coreybutler/nvm-windows/releases
![下载](image\nvm1.png)

1. nvm-noinstall.zip： 这个是绿色免安装版本，但是使用之前需要配置
2. nvm-setup.zip：这是一个安装包，下载之后点击安装，无需配置就可以使用，方便。
3. Source code(zip)：zip压缩的源码
4. Sourc code(tar.gz)：tar.gz的源码，一般用于*nix系统

我对这个目前只是简单使用，为了方便，所以下载了nvm-setup.zip文件。 然后安装

## 检测

检查是否安装成功，我们可以在新的命令窗口中输入

`nvm`

如果出现nvm版本号和一系列帮助指令，则说明nvm安装成功。
否则，可能会提示nvm: command not found
![下载](image\nvm2.png)

## 配置

因为npm的服务器在国外，所以如果下载node跟npm下载会比较慢，所以我要把下载的镜像地址改为国内的，推荐为淘宝的镜像

找到nvm安装根目录，一般在C://Users/Administrator/AppData/Roming/nvm，修改settings.txt的内容

````
root: C:\Users\Administrator\AppData\Roaming\nvm 
path: C:\Program Files\nodejs 
arch: 64 
proxy: none 
node_mirror: https://npm.taobao.org/mirrors/node/ 
npm_mirror: https://npm.taobao.org/mirrors/npm/
````

这两行代码就是链接淘宝镜像，在下载node和npm速度会快很多（不用去国外的github上下了）
node_mirror: https://npm.taobao.org/mirrors/node/
npm_mirror: https://npm.taobao.org/mirrors/npm/

## 升级


## 常用指令

````
nvm version         // 查看nvm版本
nvm install 4.6.2   // 安装node4.6.2版本（附带安装npm）
nvm uninstall 4.6.2 // 卸载node4.6.2版本
nvm list            // 查看node版本
nvm use 4.6.2       // 将node版本切换到4.6.2版本
nvm root　　　　     // 查看nvm安装路径 
nvm install latest  //下载最新的node版本和与之对应的npm版本
````

## 详细指令

##### nvm for windows是一个命令行工具，在控制台输入nvm,就可以看到它的命令用法。基本命令有：

1. `nvm arch [32|64] `： 显示node是运行在32位还是64位模式。指定32或64来覆盖默认体系结构。
2. `nvm install <version> `： 该可以是node.js版本或最新稳定版本latest。（可选[arch]）指定安装32位或64位版本（默认为系统arch）。设置[arch]为all以安装32和64位版本。在命令后面添加--insecure ，可以绕过远端下载服务器的SSL验证。
3. `nvm list [available]`： 列出已经安装的node.js版本。可选的available，显示可下载版本的部分列表。这个命令可以简写为nvm ls [available]。
4. `nvm on`： 启用node.js版本管理。
5. `nvm off`： 禁用node.js版本管理(不卸载任何东西)
1. `nvm proxy [url]`： 设置[url]为none删除代理。
1. `nvm node_mirror [url]`：设置node镜像，默认为https://nodejs.org/dist/.。我建议设置为淘宝的镜像https://npm.taobao.org/mirrors/node/
1. `nvm npm_mirror [url]`：设置npm镜像，默认为https://github.com/npm/npm/archive/。我建议设置为淘宝的镜像https://npm.taobao.org/mirrors/npm/
1. `nvm uninstall <version>`： 卸载指定版本的nodejs。
1. `nvm use [version]`： 切换到使用指定的nodejs版本。
1. `nvm root [path]`： 设置 nvm 存储node.js不同版本的目录 ,如果未设置，将使用当前目录。
1. `nvm version`： 显示当前运行的nvm版本，可以简写为nvm v

一个nodejs的安装使用流程：

````
nvm ls   // 查看目前已经安装的版本
nvm install 6.10.0  // 安装指定的版本的nodejs
nvm use 6.10.0  // 使用指定版本的nodejs
````