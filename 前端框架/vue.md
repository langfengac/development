# VUE

cdn:  <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
## MVP MVVM的区别

### MVP
![下载](..\image\vue03.png)

**View** 就是html上的dom
**Presenter** 主要从接受View互动请求，从Model取数据，业务处理后再操作dom,View展示
**Model** 数据层

**问题**：Presenter 过重，而且相当多的操作是在做dom操作
### MVVM
![下载](..\image\vue04.png)

**View**：html上的dom

**VModel**:主要由vue内部自行处理监控

**Model**:主要操作model数据，vue会自动响应到View上
## A Simple Vue Examples

<iframe src='http://localhost:4000/%E5%89%8D%E7%AB%AF%E6%A1%86%E6%9E%B6/examples1.html' width='100%' height='100%'></iframe>

`code`
```` js

<html>
<head>
    <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
</head>
<body>
    <div id='app'>
        请输入：<input type="text" @keydown.13='addlist' v-model="name" />（直接回车）
        <ul>
            <todo-item v-bind:content="item" v-for="item in list">
            </todo-item>
            <!-- <li>
                {{item.text}}
            </li> -->
        </ul>
    </div>
</body>
</html>
<script>
    //全局组件 component 这个不加s
    // Vue.component('TodoItem', {
    //     props: ['content'],
    //     template: '<li>{{content.text}}</li>'
    // })
    var app = new Vue({
        el: '#app',
        data: {
            name: '',
            list: []
        },
        //局部组件 components 这个加s
        components: {
            TodoItem: {
                props: ['content'],
                template: '<li>{{content.text}}</li>'
            }
        },
        methods: {
            addlist() {
                console.log(this.name)
                this.list.push({
                    text: this.name
                });
                this.name=''
            }
        }
    })
</script>
````

## 生命周期勾子
![下载](..\image\vue1.png)
![下载](..\image\vue2.png)

1. beforeCreate
2. create
3. beforeMount
4. mounted
5. beforeUpdate
6. updated
7. beforeDestroy
8. destroyed
9. activated
10. deactivated
11. errorCaptured
``` js
<div id='#app'></div>
//生命周期函数就是vue实例在某个时间点自动执行的函数
var vm =new Vue({
	el:'#app',
    template:'<div>hello</div>',
    data:{},
    beforeCreate:function(){
    	//初始化事件绑定等事件
        console.log('beforeCreate')
    },
    created:function(){
     //初始化外部的注入，双向绑定等内容
     console.log('created')
    },
    beforeMount:function(){
     //渲染之前
     console.log(vm.$el);//<div id='#app'></div>
     console.log('beforeMount')
    },
    mounted:function(){
    	//渲染之后
        console.log(vm.$el);//<div>hello</div>
        console.log('mounted')
    },
    beforeDestroy:function(){
    //执行实例消毁之前 vm.$destroy()
     console.log('beforeDestroy')
    },
    destroyed:function(){
    //执行实例消毁之后 vm.$destroy()
     console.log('destroyed')
    },
    beforeUpdate:function(){
    //改变data数值，虚拟dom重新渲染之前
      console.log('beforeUpdate')
    },
    updated:function(){
    //改变data数值，虚拟dom重新渲染之后
     console.log('updated')
    }
})

```
## 命令

### v-once
首次赋值后再更改 vm 实例属性值不会引起 DOM 变化,
1.0的版本为{{* name}}
2.0的版本为 v-once

``` xml
<span v-once>{{name}}</span>
```

### v-bind 属性绑定
属性绑定
1.0 直接属性上双括号`id="{{name}}"`
2.0 升级为`v-bind:id="name" `简写为 `:id="name"`

### 绑定事件  v-on:或@
如：
```js
<input type='text' v-on:click="test" />
<input tepe='text' @click='text' />
```
### v-model 双向绑定

### Vue实例
```js
var vm=new Vue({el:'#app',name:'',data:{}});
vm.$data
vm.$destory()//销毁
```
## 计算属性 监听器 方法 getset

``` js
 d:{{d}}
<br />
c:{{c}}
<br />
e:{{e()}}
整合：{{a}}{{b}}{{age}}

var vm=new Vue({
 el:'#app',
 data:{
 a:'a',
 b:'b',
 c:'c',
 age:12
 },
 watch:{
	a:function(){
     console.log('computed 计算一次')
    	this.c=this.a+this.b
    },
    b:function(){
     console.log('computed 计算一次')
    	this.c=this.a+this.b
    }
 },
 computed:{
	d:function(){
    	console.log('computed 计算一次')
    	return this.a+this.b
    }
    //或者
    d:{
        get:function(){
         console.log('computed 计算一次')
			return this.a+this.b
        },
        set:function(val){
			var arry=val.split(' ');
            this.a=arry[0]
            this.b=arry[1]
        }
    }
 },
 //
 methods:{//当任何数据变化导致虚拟dom重新渲染的时候，这个methods会重新计算（如app.$data.age=24），而computed跟watch只会跟它有关的数据变化的时候才会重新计算否则会用缓存，所以computed性能会更优而写的比watch简洁
	e:function(){
    console.log('methods 计算一次')
    return this.a+this.b
    }
 }
})
```
### 条件 列表 set方法

v-if v-else-if v-else
v-show

v-for
```js
<li v-for='(item,index) of list' :key='item.id'>
	{{item.text}}
</li>
//模版占位符
<template v-for='item of list' >
	{{item}}
</template>
//对像循环
data:{
	userInfo:{name:'mk',age:28}
}
<div v-for='(item,key,index) of userInfo'>
	{{item}}---{{key}}---{{index}}
</div>
```
### 数组操作

1、改变数据
不能直接操作下标的方式，否则view层不会跟着变化
需要过过push pop shift unshift splice sort reverse 这几个方法来操作数组
2、方法2 改为数组的引用
Info={a:1,b:2} 如果你想加个 info.c=3 这样加入view是不会响应的，要改变对像引用，Info={a:1,b:2,c:3}这样
arry=[1,2,3,4] 如果你想加个 5,用下标arry[4]=5,view层也不会响应变化的，你要改变引用 arry=[1,2,3,4,5]

3、Vue.set方法

```js
data:{
    list:[
        {a:1,
        b:2}
    ],
    userInfo:{
    	name:'dell',
        age:12
    }
}
Vue.set()
app.$set()
```

### is ref 子组件data 运用

1. is

    ````js
    
    <table>
        <tr is='row'></tr>
        如果这里用<row><row> 会导致trtd 跑到table外面来，因为在h5的规范里面table里面必须是tr，所以浏览器解析<row>这个标签时会出问题,所以这里可以用is来给vue指引，还有类似的问题的像 select 里的option
    </table>
    Vue.component("row",{
        template:<tr><td>this is row</td></tr>
    })
    ````

2. ref 引用的方法操作dom的

    ````js
	<div id='app'>
      <div ref='heilo' @click='handClick'></div>
    </div>
    new Vue({
        el:'#app',
        methods:{
            handClick:function(){
				console.log(this.$refs.hello.innerHTML)//获取到dom
            }
        }
    })
   //如果ref的是子组件，那么就不是dom而是子组件的对像引用，可以获取到子组件的类似对像信息
    ````

3. 子组件的data必须是函数，因为子组件可能被掉用多次，必须每个子组件拥有自己的数据不互相影响

### 组件参数约束 校验与非Props特性

```js
Vue.componen('child',{
	//props:['name']
    props:{
     age:Number,
     name:String,
     Val:[Number,String],
     content:{
     	type:String,
        required:false,
        default:'default value',
        validator:function(value){
         return (value.length>5)
        }
     }
    }
})
```

非Props特性
指父级组件传进来的参数，子组修的没有props接收，则传进来的参数子组件用不了，且会显示在子组件html的属性中

### 非父子组件间的传值问题（兄弟组件） =》使用总线的方式（订阅发布模式或叫观察者模式）

```js
//hrml
<div id="app">
	<child :content:="2"></child>
    <child :content:="3></child>
</div>
//html
 <div id='app'>
           <child content='Del'></child>
           <child content='All'></child>
       </div>
//js
Vue.prototype.bus=new Vue()
    Vue.component('child',{
        data:function(){
            return {
                selfContent:this.content
            }
        },
        props:['content'],
        template:'<div click="HandClick">{{selfContent}}</div>',
        methods:{
            HandClick:function(){
               this.bus.$emit('change',this.selfContent)
            }
        },
        mounted:function(){
            var _this=this;
            this.bus.$on('change',function(msg){
                _this.selfContent=msg
            })
            // this.bus.$on('change',(msg)=>{
            //     this.selfContent=msg
            // })
        }
    })
    var app=new Vue({
        el:"#app"
    })

```

### 插槽 slot

### CSS

##### 1. 过渡效果 transition

``` cs
<style>
 .fadd-enter,
 .fadd-leave-to{
  opacity:0
 }
 .faddenter-active,
 .fadd-leave-active{
 transition:opacity:1s
 }
</style>
 
 <div id='app'>
 <transition name="fadd">
 	<div v-if="show">哈哈哈</div>
 </ransition>
 </div>
 var app=new Vue({
   el:'#app',
   data:{show:true}
 })
```
v-if v-show 
显示过滤效果
![下载](..\image\vuecss1.png)
隐藏过渡效果
![下载](..\image\vuecss2.png)

#### 2. Animate.css运用

#### 3. velocityjs
http://www.mrfront.com/docs/velocity.js/index.html

#### 4. 多组件 或 多节点间动画效果  动态组件

1. 多组件间的 动态组件 component

``` js
	<div id='app'>
    	<transition model='out-in'>//这里有out-in跟in-out。 out-in 指先让out的节点运行完，再运行in的节点。如果不写这个model就是一起运行
    	<component :is='type'><c/component>
        </transition>
    </div>
    Vue.component('child1'{template:'<div>我是child-1</div>'})
    Vue.component('child2'{template:'<div>我是child-2</div>'})
    var app=new Vue({
    	data:{
        type:'child1'
        },
        methods:{
            toggleHandle:function(){
				this.type=this.type==='child1'?'child2':'child1'
            }
        }
    })

```

2. 多元素之间的过滤动画
	如果发现没有变化，可能是vue的虚拟复用导致的，可以给节点加key 让他们不复用

#### 列表的动态效果 transition-group

``` js
    <style>
        这里的样式效果跟transition那边是一样的
    </style>
    <transition-group name='fade'>
        <div v-for="(item) of list" :key='item.id'> 1</div>
    </transition-group>
    <input type="button" value="add">//按钮添加list数据

```

#### 动画效果封装

使用插槽的方式写个组件

``` js

Vue.component('fade',{template:"<transition><slot></slot></transition>")
```


### 表达式