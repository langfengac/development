# WebApi



## 生命周期

要了解一套框架或新技术，要先了解一下它的生命周期，接下来我们来看一下webapi的生命周期。


## 路由

1. action 自定义
``` js
$.ajax({
                type: "post",
                contentType: 'application/json',
                url: "http://localhost:52497/api/user/Actest",
                data: JSON.stringify({ NAME: "Jim", DES: "备注" }),
                success: function (data, status) {
                    if (status == "success") {
                        if (!data.bRes) {
                            alert("登录失败");
                            return;
                        }
                        alert("登录成功");
                        //登录成功之后将用户名和用户票据带到主界面
                        window.location = "/Default/Index?UserName=" + data.UserName + "&Ticket=" + data.Ticket;
                    }
                }  
            });
```

2. 路由特性

``` js
$.ajax({
                type: "post",
                contentType: 'application/json',
                url: "http://localhost:52497/Order/SaveData",
                data: JSON.stringify({ NAME: "Jim", DES: "备注" }),
                success: function (data, status) {
                    if (status == "success") {
                        if (!data.bRes) {
                            alert("登录失败");
                            return;
                        }
                        alert("登录成功");
                        //登录成功之后将用户名和用户票据带到主界面
                        window.location = "/Default/Index?UserName=" + data.UserName + "&Ticket=" + data.Ticket;
                    }
                }  
            });
```

`url: "http://localhost:52497/api/user/Order/SaveData",`