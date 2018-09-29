# VUE

## 生命周期
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

### 表达式