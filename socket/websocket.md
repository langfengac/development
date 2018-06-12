# webscket

## 介绍
websocket 主要是为了满足人们实时通信的需求而产生的，在传统的web中，要实现通讯，我们需要基本http协义不断发送请求（长链接或轮询），但这种方式浪费带宽（request/response的head比较大，如果cookies再大，就更消耗了），又消耗服务器CPU占用（没信息也要不停的请求或等待）！。
websocket是html5出的协议，提供了浏览器与服务器进行全双工通讯的网络技术，属于<a href="#附件">应用层协议</a>.它基于tcp传输协义，并复用http的握手通道。

## 特点

1. 通过TCP一次握手就可以建立连接。  而HTTP协议需要三次握手。
2. HTTP中服务器永远是被动的，即每次只有客户端发出请求，服务器才会响应。 但是WebSocket协议中，服务器是可以主动的向客户端传递数据。这样就避免了轮询的问题。
3. WebSocket需要浏览器、服务器同时支持才可以使用，而http协议是普遍支持的。 且WebSocket是一种新的协议，只是目前为了兼容性，必须要建立在http的基础上发起请求，如只用WebSocket协议名将不再是http:而是ws:。
4. 同样地，WebSocket也是基于TCP协议的。
5. Html5是指的一系列新的API，或者说新规范，新技术。Http协议本身只有1.0和1.1，而且跟Html本身没有直接关系。。通俗来说，你可以用HTTP协议传输非Html数据，就是这样=。= 再简单来说，层级不一样。

## websocket 和 http 协议区别

　　首先要知道的时 websocket 和 http是不同的两个协议，最大的区别在于---http协议是被动的，而websocket协议是主动的。 所谓被动就是说只有客户端发起请求服务器端才会给出响应，而websocket显然就是说可以由服务器端来主动给数据，也许你并没有请求。

#### 1)首先我们来看个典型的 Websocket 握手

``` json
GET /chat HTTP/1.1
Host: server.example.com
Upgrade: websocket
Connection: Upgrade
Sec-WebSocket-Key: x3JJHMbDL1EzLkh9GBhXDw==
Sec-WebSocket-Protocol: chat, superchat
Sec-WebSocket-Version: 13
Origin: http://example.com
```
熟悉HTTP的童鞋可能发现了，这段类似HTTP协议的握手请求中，多了几个东西。我会顺便讲解下作用。

```json
Upgrade: websocket
Connection: Upgrade
```
这个就是Websocket的核心了，告诉 Apache 、 Nginx 等服务器：注意啦，我发起的是Websocket协议，快点帮我找到对应的助理处理~不是那个老土的HTTP。

```json
Sec-WebSocket-Key: x3JJHMbDL1EzLkh9GBhXDw==
Sec-WebSocket-Protocol: chat, superchat
Sec-WebSocket-Version: 13
```
首先， Sec-WebSocket-Key 是一个 Base64 encode 的值，这个是浏览器随机生成的，告诉服务器：泥煤，不要忽悠窝，我要验证尼是不是真的是Websocket助理。

然后， Sec_WebSocket-Protocol 是一个用户定义的字符串，用来区分同URL下，不同的服务所需要的协议。简单理解：今晚我要服务A，别搞错啦~

最后， Sec-WebSocket-Version 是告诉服务器所使用的 Websocket Draft（协议版本），在最初的时候，Websocket协议还在 Draft 阶段，各种奇奇怪怪的协议都有，而且还有很多期奇奇怪怪不同的东西，什么Firefox和Chrome用的不是一个版本之类的，当初Websocket协议太多可是一个大难题。。不过现在还好，已经定下来啦~大家都使用的一个东西~ 脱水： 服务员，我要的是13岁的噢→_→

```json
HTTP/1.1 101 Switching Protocols
Upgrade: websocket
Connection: Upgrade
Sec-WebSocket-Accept: HSmrc0sMlYUkAGmm5OPpG2HaGWk=
Sec-WebSocket-Protocol: chat
```
这里开始就是HTTP最后负责的区域了，告诉客户，我已经成功切换协议啦~


## websocket api

### 客户端api

####  webSocket.readyState
readyState属性返回实例对象的当前状态，共有四种。

```json
CONNECTING：值为0，表示正在连接。
OPEN：值为1，表示连接成功，可以通信了。
CLOSING：值为2，表示连接正在关闭。
CLOSED：值为3，表示连接已经关闭，或者打开连接失败。
```

#### webSocket.bufferedAmount
实例对象的bufferedAmount属性，表示还有多少字节的二进制数据没有发送出去。它可以用来判断发送是否结束。

``` json
var data = new ArrayBuffer(10000000);
socket.send(data);

if (socket.bufferedAmount === 0) {
  // 发送完毕
} else {
  // 发送还没结束
}
```
#### webSocket.send()

实例对象的send()方法用于向服务器发送数据。

发送文本的例子。
``` js
ws.send('your message');
```

发送 Blob 对象的例子。
``` js
var file = document
  .querySelector('input[type="file"]')
  .files[0];
ws.send(file);
```
发送 ArrayBuffer 对象的例子。

``` js
// Sending canvas ImageData as ArrayBuffer
var img = canvas_context.getImageData(0, 0, 400, 320);
var binary = new Uint8Array(img.data.length);
for (var i = 0; i < img.data.length; i++) {
  binary[i] = img.data[i];
}
ws.send(binary.buffer);
```
### 服务端

服务端包含
net：一般处理程序、webapi
nodejs:Socket.IO

``` json
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace SimpleWebSocket
{
    /// <summary>
    /// 
    /// </summary>
    public class WebSocketHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //检查 查询是否是WebSocket请求
            if (HttpContext.Current.IsWebSocketRequest)  
            {
                //如果是，我们附加异步处理程序
                context.AcceptWebSocketRequest(WebSocketRequestHandler);
            }
        }

        public bool IsReusable { get { return false; } }

        //异步请求处理程序
        public async Task WebSocketRequestHandler(AspNetWebSocketContext webSocketContext)
        {
            //获取当前的WebSocket对象
            WebSocket webSocket = webSocketContext.WebSocket;

            /*
             * 我们定义一个常数，它将表示接收到的数据的大小。 它是由我们建立的，我们可以设定任何值。 我们知道在这种情况下，发送的数据的大小非常小。
            */
            const int maxMessageSize = 1024;

            //received bits的缓冲区
            var receivedDataBuffer = new ArraySegment<Byte>(new Byte[maxMessageSize]);

            var cancellationToken = new CancellationToken();

            //检查WebSocket状态
            while (webSocket.State == WebSocketState.Open)
            {
                //读取数据 
                WebSocketReceiveResult webSocketReceiveResult = await webSocket.ReceiveAsync(receivedDataBuffer, cancellationToken);

                //如果输入帧为取消帧，发送close命令
                if (webSocketReceiveResult.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, cancellationToken);
                }
                else
                {
                        byte[] payloadData = receivedDataBuffer.Array.Where(b => b != 0).ToArray();

                        //因为我们知道这是一个字符串，我们转换它
                        string receiveString = System.Text.Encoding.UTF8.GetString(payloadData, 0, payloadData.Length);

                        //将字符串转换为字节数组.
                        var newString = String.Format("Hello, " + receiveString + " ! Time {0}", DateTime.Now.ToString());
                        Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(newString);

                        //发回数据
                        await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, cancellationToken);
                }

            }
        }
    }

}
```

## 延伸 socket与websocket

Socket 其实并不是一个协议。它工作在 OSI 模型会话层（第5层），是为了方便大家直接使用更底层协议（一般是 TCP 或 UDP ）而存在的一个抽象层。

最早的一套 Socket API 是 Berkeley sockets ，采用 C 语言实现。它是 Socket 的事实标准，POSIX sockets 是基于它构建的，多种编程语言都遵循这套 API，在 JAVA、Python 中都能看到这套 API 的影子。

下面摘录一段更容易理解的文字（来自 http和socket之长连接和短连接区别）：

``` js
Socket是应用层与TCP/IP协议族通信的中间软件抽象层，它是一组接口。在设计模式中，Socket其实就是一个门面模式，它把复杂的TCP/IP协议族隐藏在Socket接口后面，对用户来说，一组简单的接口就是全部，让Socket去组织数据，以符合指定的协议。
```
![下载](..\image\socket_1.gif)
![下载](..\image\socket_2.gif)
```js
主机 A 的应用程序要能和主机 B 的应用程序通信，必须通过 Socket 建立连接，而建立 Socket 连接必须需要底层 TCP/IP 协议来建立 TCP 连接。建立 TCP 连接需要底层 IP 协议来寻址网络中的主机。我们知道网络层使用的 IP 协议可以帮助我们根据 IP 地址来找到目标主机，但是一台主机上可能运行着多个应用程序，如何才能与指定的应用程序通信就要通过 TCP 或 UPD 的地址也就是端口号来指定。这样就可以通过一个 Socket 实例唯一代表一个主机上的一个应用程序的通信链路了。
```
而 WebSocket 则不同，它是一个完整的 应用层协议，包含一套标准的 API 。

所以，从使用上来说，WebSocket 更易用，而 Socket 更灵活。

## 附件
![下载](..\image\websocket_.gif)

## 源码
源码可自行上git上查看demo