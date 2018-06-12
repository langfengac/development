# C#

## 注入

依赖注入分类：

1. setter 注入（Setter Injection）
uml图

2. 构造器注入（Constructor Injection）
uml图

3. 依赖获取（Dependency Locate）
uml图

#### 扩展点：反射与注入

## IOC

## 委托

## 反射 Reflection



.net中获取运行时类型信息的方式，net的应用程序由几个部分组成：程序集Assembly  模块Module 类型class ，而反射提供一种编程的方式，让程序员可以在程序员可以在程序运行期获得这几个组成部份的相关信息，例如：



 1. Assembly类可以获得正在运行的装配件信息，也可以动态的加载装配件，以及在装配件中查找类型信息，并创建该类型的实例。

 2. Type类可以获得对象的类型信息，此信息包含对象的所有要素：方法、构造器、属性等等，通过Type类可以得到这些要素的信息，并且调用之。

 3. MethodInfo包含方法的信息，通过这个类可以得到方法的名称、参数、返回值等，并且可以调用之。



诸如此类，还有FieldInfo、EventInfo等等，这些类都包含在System.Reflection命名空间下。

反射的定义：审查元数据并收集关于它的类型信息的能力。元数据（编译以后的最基本数据单元）就是一大堆的表，当编译程序集或者模块时，编译器会创建一个类定义表，一个字段定义表，和一个方法定义表等。

公共语言运行库（CLR）加载器管理应用程序域，这些域在拥有相同应用程序范围的对象周围形成了确定边界。这种管理包括将每个程序集加载到相应的应用程序域以及控制每个程序集中类型层次结构的内存布局。
System.reflection命名空间包含的几个类，允许你反射（解析）这些元数据表的代码
System.Reflection.Assembly
System.Reflection.MemberInfo
System.Reflection.EventInfo
System.Reflection.FieldInfo
System.Reflection.MethodBase
System.Reflection.ConstructorInfo
System.Reflection.MethodInfo
System.Reflection.PropertyInfo
System.Type
以下是上面几个类的使用方法：
（1）使用Assembly定义和加载程序集，加载在程序集清单中列出模块，以及从此程序集中查找类型并创建该类型的实例。
（2）使用Module了解包含模块的程序集以及模块中的类等，还可以获取在模块上定义的所有全局方法或其他特定的非全局方法。
（3）使用ConstructorInfo了解构造函数的名称、参数、访问修饰符（如pulic 或private）和实现详细信息（如abstract或virtual）等。使用Type的GetConstructors或 GetConstructor方法来调用特定的构造函数。
（4）使用MethodInfo了解方法的名称、返回类型、参数、访问修饰符（如pulic 或private）和实现详细信息（如abstract或virtual）等。使用Type的GetMethods或GetMethod方法来调用特定的方法。
（5）使用FiedInfo了解字段的名称、访问修饰符（如public或private）和实现详细信息（如static）等，并获取或设置字段值。
（6）使用EventInfo了解事件的名称、事件处理程序数据类型、自定义属性、声明类型和反射类型等，添加或移除事件处理程序。
（7）使用PropertyInfo了解属性的名称、数据类型、声明类型、反射类型和只读或可写状态等，获取或设置属性值。
（8）使用ParameterInfo了解参数的名称、数据类型、是输入参数还是输出参数，以及参数在方法签名中的位置等。
## 索引器
索引器可以近视为带参数的属性，但名称以this，也实现get set 的访问器。
注：`this`表示操作本对象的数组或集合成员，可以简单把它理解成索引器的名字，因此索引器不能具有用户定义的名称。索引器参数可以不止一个，类型也不限于int，几乎可以是任意类型

例子：
``` cpp
 语法：
 [修饰符] 数据类型 this[索引类型 index]
    {
      get{//获得属性的代码}
      set{ //设置属性的代码}
    }
 public class Student{
  List<string> str=new List<string>();
  public string this[string index]{
    get {return str[index]}
	set { list[index]=value}
  }
 }
```

## 接口

接口使用interface关键字进行定义，可由方法、属性、事件、索引器或这四种成员类型的任意组合构成

1. 接口类似于抽象基类，不能直接实例化接口；接口中的方法都是抽象方法，实现接口的任何非抽象类型都必须实现接口的所有成员：
当显式实现该接口的成员时，实现的成员不能通过类实例访问，只能通过接口实例访问。
当隐式实现该接口的成员时，实现的成员可以通过类实例访问，也可以通过接口实例访问，但是实现的成员必须是公有的。
（注：后面的demo会做说明）

2. 接口不能包含常量、字段、运算符、实例构造函数、析构函数或类型、不能包含静态成员。

3. 接口成员是自动公开的，且不能包含任何访问修饰符。

4. 接口自身可从多个接口继承，类和结构可继承多个接口，但接口不能继承类。

### 什么是显式 什么是隐式

一般情况，当类或者结构要实现的是单个接口，可以使用隐式实现。
如果类或者结构继承了多个接口且接口中具有相同名称成员时，就要用到显式实现，当显式实现方式存在时，隐式实现方式就失效了。

``` cpp
interface IPro
 {
  void Fun();
 }
 interface IAPro
 {
  void Fun();
 }
 class Pro : IPro, IAPro
 {
  void IPro.Fun() //显式实现接口IPro
  {
   Console.WriteLine("I am IPro Fun.");
  }
  void IAPro.Fun() //显式实现接口IAProgram
  {
   Console.WriteLine("I am IAPro Fun.");
  }
  //public void Fun() //隐式实现接口
  //{
  // Console.WriteLine("I am Pro Fun.");
  //}
  staticvoid Main(string[] args)
  {
   //IPro p = new Pro();
   //p.Fun();
   //IAPro ap = new Pro();
   //ap.Fun();
   Pro pro =new Pro();
   ((IPro)pro).Fun();
   ((IAPro)pro).Fun();
   Console.Read();
  }
 }


```
结果为：
`I am IPro Fun.`
`I am IAPro Fun.`

### 接口继承

首先，类继承不仅是说明继承，而且也是实现继承；而接口继承只是说明继承。

也就是说，派生类可以继承基类的方法实现，而派生的接口只继承了父接口的成员方法说明，而没有继承父接口的实现，

其次，C#中类继承只允许单继承，但是接口继承允许多继承，一个子接口可以有多个父接口。

接口可以从零或多个接口中继承。从多个接口中继承时，用":"后跟被继承的接口名字，多个接口名之间用","分割。

被继承的接口应该是可以访问得到的，比如从private 类型或internal 类型的接口中继承就是不允许的。

接口不允许直接或间接地从自身继承。和类的继承相似，接口的继承也形成接口之间的层次结构。


``` cpp
interface IProgram
{
 void Fun();
}
interface IAProgram:IProgram
{
}
class Program : IAProgram
{
 void IProgram.Fun()
 {
  Console.WriteLine("I am IProgram Fun.");
 }
 staticvoid Main(string[] args)
 {
  Program pro =new Program();
  ((IAProgram)pro).Fun();
  Console.Read();
 }
}
```
### 接口覆盖

``` cpp
interface IProgram
 {
  void Fun();
 }
 abstractclass AProgram : IProgram
 {
  publicabstractvoid AFun();
  void IProgram.Fun()
  {
   AFun();
  }
 }
 class Program:AProgram
 {
  publicoverridevoid AFun()
  {
   Console.WriteLine("I am AProgram.");
  }
  staticvoid Main(string[] args)
  {
   IProgram pro =new Program();
   pro.Fun();
   Console.Read();
  }
 }
//结果：I am Aprogram.
```
通过断点，可以看到，当执行pro.Fun();时，首先会跳到接口的实现方法里，然后去调用抽象函数的实现方法，当抽象函数的方法实现后，再回到接口的实现方法，直到执行完成。

``` cpp
interface IProgram
{
 void Fun();
}
class AProgram : IProgram
{
 publicvirtualvoid AFun() //注意这里是虚函数
 {
  Console.WriteLine("I am virtual AFun.");
 }
 void IProgram.Fun()
 {
  AFun();
 }
}
class Program:AProgram
{
 publicoverridevoid AFun() //这里是Override重写
 {
  Console.WriteLine("I am override AFun.");
 }
 staticvoid Main(string[] args)
 {
  IProgram pro =new Program();
  pro.Fun();
  Console.Read();
 }
}
```

这时，我们发现，执行的顺序和上一个例子是相同的。所以结果为：`I am override AFun.`

由此，我们可以继续联想，当我们把override关键字，换成new呢？是不是也是同样的结果，还是和我们以前讲的例子一样，是隐藏呢？

``` cpp
interface IProgram
 {
  void Fun();
 }
 class AProgram : IProgram
 {
  publicvirtualvoid AFun()
  {
   Console.WriteLine("I am virtual AFun.");
  }
  void IProgram.Fun()
  {
   AFun();
  }
 }
 class Program:AProgram
 {
  publicnewvoid AFun()
  {
   Console.WriteLine("I am new AFun.");
  }
  staticvoid Main(string[] args)
  {
   Program pro =new Program();
   ((IProgram)pro).Fun();
   pro.AFun();
   Console.Read();
  }
 }
```
结果为：`I am virtual AFun.`

`I am new AFun.`
由于前面已经讲过了，这里不在对此进行分析，由此我们可知使用New关键字是对其进行隐藏，当对接口实现的方法里调用的是虚方法时，和类的执行过程是一样的。

### 接口和抽象类的区别

接口用于规范，抽象类用于共性。
接口中只能声明方法，属性，事件，索引器。而抽象类中可以有方法的实现，也可以定义非静态的类变量。
抽象类是类，所以只能被单继承，但是接口却可以一次实现多个。
抽象类可以提供某些方法的部分实现，接口不可以。
抽象类的实例是它的子类给出的。接口的实例是实现接口的类给出的。
在抽象类中加入一个方法，那么它的子类就同时有了这个方法。而在接口中加入新的方法，那么实现它的类就要重新编写（这就是为什么说接口是一个类的规范了）。
接口成员被定义为公共的，但抽象类的成员也可以是私有的、受保护的、内部的或受保护的内部成员（其中受保护的内部成员只能在应用程序的代码或派生类中访问）。
此外接口不能包含字段、构造函数、析构函数、静态成员或常量。

### C#中的接口和类有什么异同

异：
不能直接实例化接口。
接口不包含方法的实现。
接口可以实现多继承，而类只能是单继承。
类定义可在不同的源文件之间进行拆分。
同：
接口、类和结构可从多个接口继承。
接口类似于抽象基类：继承接口的任何非抽象类型都必须实现接口的所有成员。
接口可以包含事件、索引器、方法和属性。
一个类可以实现多个接口。

