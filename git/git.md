# git

## 概述

### 暂存区

### 工作区

## linux常用命令

mkdir rm vim 

### 文件操作

### 内容编辑

## 仓库

推送到远程仓库：git push origin master

获取更新远程仓库最新版本到本地：gitpull 和 git fetch

1. git pull

git pull 获取远程仓库最新版本，并执行merge操作

2. git fetch

git fetch 获取远程仓库最新版本，但不会执行merge操作，所以git fetch后会执行`git merge origin/master`
例如：
git fetch origin master
git log -p master.. origin/master
git merge origin/master

或者
git fetch origin master-tmp
git diff tmp
git merge tmp

copy远程仓库到本地：git clone

## 分支

查看分支：git branch

创建分支：git branch <name>

切换分支：git checkout <name>

创建+切换分支：git checkout -b <name>

合并某分支到当前分支：git merge <name>

删除分支：git branch -d <name>

强制删除分支：git branch -D <name>
当新建出来的分支还没合并就要直接删除的时候会提示分支还没合并。这里面要用-D进行强制删除

暂存bug分支：git stash


### 命令
1. 创建分支 `git branch dev`
切换分支 `git checkout dev`

快捷操作 `git checkout -b dev` 同义为：创建分支并切换到分支

2. 分支暂存:



## 标签

## 服务器搭建


## 工作流


