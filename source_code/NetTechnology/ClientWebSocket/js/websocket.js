 
/*
 *websocket  "ws://localhost:9998/echo"
 *  HTTP 接口路径为 http://10.1.7.102/mapweaver/consult/接口名称
    WebService 接口路径为 http://10.1.7.102/mapweaver/ws/ConsultationWebService?wsdl
    WebSocket 接口路径为  http://10.1.7.102:80/mapweaver/ConsultationSocket
 */
var system = system || {}; 
system.WebSocketModel = function (url) {
   var _options = {
        url: url,//接口地址
        onopen: null,//握手后回调方法
        onmessage: null,//收到消息 回调方法
        onclose: null,//关闭握手 回调方法
        onerror: null,//接口报错，回调方法
        heart:  {
           enable: false,
           sendmessage: "",
           intervaltime: 5000
       }//是否开启心跳
    };
    
        var ws;
    var initialize = function (options) {
        _options = $.extend(true, _options, options);
        var time;
        // 初始化一个 WebSocket 对象
        ws = new WebSocket(_options.url);
        // 建立 web socket 连接成功触发事件
        ws.onopen = function () {
            console.log("联接成功...");
            //是否开启心跳
            if (_options.heart && _options.heart.enable) {
                time = setInterval(function () {
                    send(_options.heart.sendmessage);
                }, 5000);
            }
        };

        // 接收服务端数据时触发事件
        ws.onmessage = function (evt) {
            var received_msg = evt.data;
            console.log("数据已接收...");
            if (_options.onmessage) {
                _options.onmessage(received_msg);
            }
        };

        // 断开 web socket 连接成功触发事件
        ws.onclose = function (es) {
            console.log("连接已关闭...");
            if (_options.onclose) {
                _options.onclose(es);
            }
            if (_options.heart || _options.heart.enable) {
                clearInterval(time);//关闭心跳
            }
        };
        //接口报错
        ws.onerror = function (es) {
            console.log("出错...");
        }
        this.ws = ws;
    }
    var send = function (value) {
         
        if (ws.readyState === WebSocket.OPEN) {
            ws.send(value);
        } else {
            var time = setInterval(function () {
                if (ws.readyState === WebSocket.OPEN) {
                    clearInterval(time);
                    //执行发布
                    ws.send(value);
                }
            }, 500)
        }
    }
    var close = function () {
        ws.close();
    }
    return {
        send: send,
        close: close,
        init: initialize
    }
}