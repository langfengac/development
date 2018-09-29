# 消息队列-RabbitMQ

Rabbit MQ 是建立在强大的Erlang OTP平台上，因此安装Rabbit MQ的前提是安装Erlang。

使用的原因：http://www.doc88.com/p-5826232080382.html

## 应用场景

 1）信息的发送者和接收者如何维持这个连接，如果一方的连接中断，这期间的数据如何方式丢失？

 2）如何降低发送者和接收者的耦合度？

 3）如何让Priority高的接收者先接到数据？

 4）如何做到load balance？有效均衡接收者的负载？

 5）如何有效的将数据发送到相关的接收者？也就是说将接收者subscribe 不同的数据，如何做有效的filter。

 6）如何做到可扩展，甚至将这个通信模块发到cluster上？

 7）如何保证接收者接收到了完整，正确的数据？

  AMDQ协议解决了以上的问题，而RabbitMQ实现了AMQP。
  
## 安装 Erlang(语言环境)


1. 下载Erlang [官网下载](http://www.erlang.org/downloads)
注：此处下载的erlang的版本必须跟rabbit的版本对应，所以下载安装前要先看一下rabbit的[支持版本](http://www.rabbitmq.com/which-erlang.html)

**支持版本：**

![下载](..\image\rabbit02.png)

**下载Erlang：**

![下载](..\image\rabbit01.png)


2. 配置到环境变量
安装地址为：D:\Program Files\RabbitMQErlang
环境变量设置Path为：D:\Program Files\RabbitMQErlang\erl9.3\bin

3. 检测环境安装

cmd中运行 erl
![下载](..\image\rabbit03.png)

## 安装 Rebbit

1. 下载地址 http://www.rabbitmq.com/download.html

## 了解RabbitMQ

1. **Message**：消息，包含消息头（即附属的配置信息）和消息体（即消息的实体内容）
2. **Publisher**：生产者，向交换机发布消息的主体
3. **Exchange**：交换机，用来接收生产者发送的消息并将这些消息路由给服务器中的队列
4. **Binding**：绑定，用于给Exchange和Queue建立关系，就是我们熟知的配对的红娘
5. **Queue**：消息队列，用来保存消息直到发送给消费者。它是消息的容器，也是消息的终点。一个消息可投入一个或多个队列。消息一直在队列里面，等待消费者连接到这个队列将其取走。
6. **Connection：**连接
7. **Channel：**通道，MQ与外部打交道都是通过Channel来的，发布消息、订阅队列还是接收消息，这些动作都是通过Channel完成；简单来说就是消息通过Channel塞进队列或者流出队列
8. **Consumer**：消费者，从消息队列中获取消息的主体
9. **Virtual Host: **虚拟主机，表示一批交换器、消息队列和相关对象。虚拟主机是共享相同的身份认证和加密环境的独立服务器域。每个 vhost 本质上就是一个 mini 版的 RabbitMQ 服务器，拥有自己的队列、交换器、绑定和权限机制。vhost 是 AMQP 概念的基础，必须在连接时指定，RabbitMQ 默认的 vhost 是 /
10. **Broker**：消息队列服务器实体

## Exchange 模式

### Direct 策略

### Fanout策略

### Topic策略

### Header策略

这个实际上用得不多，它是根据Message的一些头部信息来分发过滤Message，忽略routing key的属性，如果Header信息和message消息的头信息相匹配

