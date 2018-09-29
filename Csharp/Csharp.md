# `C#`



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
 委托的主要作用是可以把方法，做为参数进行传递
 委托是一个类，它定义了方法的类型，使得可以将方法当作另一个方法的参数来进行传递，这种将方法动态地赋给参数的做法，可以避免在程序中大量使用If-Else(Switch)语句，同时使得程序具有更好的可扩展性。
 委托（Delegate）特别用于实现事件和回调方法。所有的委托（Delegate）都派生自 System.Delegate 类。
 注：
``` cs
 //1. 定义委托
 //语法
delegate <return type> <delegate-name> <parameter list>
 //examples
public delegate int Add(int num1,int num2);//有返回值

public delegate void ConvertNum(string result);//无返回值

 //2.实例化委托

 ConvertNum cn=new ConvertNum(Test);
 public void Test(string ss){
 Console.Write(ss);
 };

 //使用
 cn("你好");

```
### 委托几种方式

 1. 推断
 委托的推断，并没有new 委托这个步骤，而是直接将Function 指定给委托。

 2. 匿名
 不用显示的指定方法名，因为根本没有方法，而是指定的匿名方法。匿名方法在.NET 中提高了代码的可读性和优雅性。
 对于更多操作较少的方法直接写为匿名函数，这样会大大提高代码的可读性。这里有两个值得注意的地方:
 第一,不能使用跳转语句跳转到该匿名方法外
 第二 不能使用ref,out修饰的参数

 3. 多播（委托链）
多播委托，在一个委托上指定多个执行方法，这是在程序中可以行的

```cs
 public delegate void WriteStr(string name);
        static void Main(string[] args)
        {
            Console.WriteLine("////////1.推断///////////////");
            WriteStr writeStr = SayHello;
            WriteStr writeStr1 = NiceToMetting;
            WriteStr writeStrList = writeStr + writeStr1;
            writeStrList("李旺");
            Console.WriteLine("////////3.多播///////////////");
            WriteStr writeStrNew = new WriteStr(SayHello);
            writeStrNew += NiceToMetting;
            writeStrNew("旺旺");

            Console.WriteLine("////////委托链+= -=操作///////////////");

            writeStrNew -= NiceToMetting;
            writeStrNew("旺旺");

            Console.WriteLine("////////2.匿名///////////////");

            WriteStr anonymous = delegate (string name)
              {
                  Console.WriteLine(name + ",你好呀");
              };
            WriteStr anonymous1 = delegate (string ss)
            {
                Console.WriteLine(ss + "，很高兴见到你");
            };
            anonymous += anonymous1;
            anonymous("旺仔");

            Console.ReadLine();

        }
        public static void SayHello(string Name) {
            Console.WriteLine(Name + "，hello ,how are you!");
        }
        public static void NiceToMetting(string ss)
        {
            Console.WriteLine(ss + ",Nice to meet you!");
        }
```


### 事件

事件就是委托的一层封装，他规则定委托不能随便赋值等操作，破坏对像的封装性，在类的内部，不管你声明它是public还是protected，它总是private的。在类的外部，注册“+=”和注销“-=”的访问限定符与你在声明事件时使用的访问符相同。

看代码：
```cs
namespace BoserverModel
{
    public class Heater
    {
        public int temperature;
        public delegate void Warning(int value);
        public Warning _warning;
        public event Warning WarningEventHandler;

        public void AddWater()
        {
            for (int i = 0; i <=100; i++)
            {
                temperature = i;
                if (temperature>=95)
                {
                    if (_warning != null)
                    {
                        Console.Write("委托");
                        _warning(temperature);
                    }
                    if (WarningEventHandler!=null)
                    {
                        Console.Write("事件");
                        WarningEventHandler(temperature);
                    }
                }
            }
        }

    }
    public class Alarm
    {
        public void MarkAlter(int temmerature)
        {
            Console.WriteLine(string.Format("警靠警靠警靠！~水温{0}度拉", temmerature));
        }
    }
    public class DisPlay
    {
        public void ShowTem(int temmerature)
        {
            Console.WriteLine(string.Format("水温{0}度拉，很热了，关闭", temmerature));
        }
    }
}

class Program
    {
        static void Main(string[] args)
        {
            Heater heater = new Heater();
            //ater.WarningEventHandler = new Alarm().MarkAlter;//事件，如果以赋值的形式，会报错，只能以增减的形式
            heater.WarningEventHandler += new Alarm().MarkAlter;//事件
            heater._warning = new DisPlay().ShowTem;//委挺可以+= -=的注册委托跟=赋值委托
            heater.AddWater();
            Console.ReadLine();
        }
    }

```
![下载](..\image\csharp04.png)

使如：`public event GreetingDelegate MakeGreet;`
通过反编译
```cs
private GreetingDelegate MakeGreet; //对事件的声明 实际是 声明一个私有的委托变量

[MethodImpl(MethodImplOptions.Synchronized)]
public void add_MakeGreet(GreetingDelegate value){
    this.MakeGreet = (GreetingDelegate) Delegate.Combine(this.MakeGreet, value);
}

[MethodImpl(MethodImplOptions.Synchronized)]
public void remove_MakeGreet(GreetingDelegate value){
    this.MakeGreet = (GreetingDelegate) Delegate.Remove(this.MakeGreet, value);
}
```
MakeGreet事件确实是一个GreetingDelegate类型的委托，只不过不管是不是声明为public，它总是被声明为private。另外，它还有两个方法，分别是add_MakeGreet和remove_MakeGreet，这两个方法分别用于注册委托类型的方法和取消注册。实际上也就是： “+= ”对应 add_MakeGreet，“-=”对应remove_MakeGreet。而这两个方法的访问限制取决于声明事件时的访问限制符。

而winform中的事件，而且扩展了里面的传参Object对像，也就是被监控的对像(Subject),跟EventArgs 对象包含了Observer所感兴趣的数据
详细可看这个文章：http://www.cnblogs.com/JimmyZhang/archive/2007/09/23/903360.html

### Func（泛型委托）

最多传16个参数，必须有返回值自定义，但不能为void.

``` cs
三种方式实现
//1、实例化
Func<int, int, string> func = new Func<int, int, string>(MultiMethod);
//2、直接带方法
Func<string, string, string, string> fc = strMethod;
//3、匿名委托
Func<string, string, string, string> fce = delegate (string a, string b, string c) { return a + b; };
//4、Lamdba表达式
Func<string, string, string, string> fce = (x,y,z)=>{return x+y;};

public static string strMethod(string a, string b,string c)
        {
            return a+b+c;
        }
public static string MultiMethod(int a, int b)
        {
            return (a * b).ToString();
        }
```

### Action（泛型委托）

最多传16个参数，没有返回值 `(实现方法与Func方法相似)`

### perdicate（泛型委托）

只能传`一个参数`，返回值类型为bool `(实现方法与Func方法相似)`
``` cs

// 摘要:
//     表示定义一组条件并确定指定对象是否符合这些条件的方法。
//
// 参数:
//   obj:
//     要按照由此委托表示的方法中定义的条件进行比较的对象。
//
// 类型参数:
//   T:
//     要比较的对象的类型。
//
// 返回结果:
//     如果 obj 符合由此委托表示的方法中定义的条件，则为 true；否则为 false。
public delegate bool Predicate<in T>(T obj);
#以上是系统类自带的，我们可以看到，他就是帮我们写了一个委托
#然后我们的实现方式是：
 方法一
 Predicate<int> method = (x) => { return x > 3; };
 方法二
 Predicate<int> method = delegate(int a){ return a > 3; };
 方法三
 Predicate<int> methodaa= sge;

 public static bool sge(int a) { return a > 0; }
```

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



接口使用interface关键字进行定义，可由**方法、属性、事件、索引器**或这四种成员类型的任意组合构成

``` cs
	public delegate void TestDelegate();//定义委托
    // 接口1
    interface ITestInterface
    {
        String Name { get; set; }//定义属性
        List<int> Set { get; set; }//定义属性
        void GetName(string ID);//定义方法

        int this[int index] { get;set; }//定义索引器：注意只能一个类只能有一个索引，除非派生类继承了多个接口，接口里面有索引，这个现实索引必须显示的现实（this前面加入接口名）

        event TestDelegate TestEvent;//定义事件
        void FireAway();//定义方法
    }
    //接口2
    interface ITestInterfaceTwo
    {
        int this[int index] { get; set; }
    }
```

``` cs
 public class TestClass : ITestInterface,ITestInterfaceTwo
    {
        public int this[int index] {
            get => Set[index];
            set => Set[index] = value;
        }
        int ITestInterfaceTwo.this[int index] {
            get => Set[index];
            set => Set[index]=value;
        }

        public string Name {
            get;
            set;
        }
        public List<int> Set {
            get;
            set;
        }

        public event TestDelegate TestEvent;

        public void FireAway()
        {
            if (TestEvent != null)
            {
                TestEvent();
            }
        }

        public void GetName(string ID)
        {
            Console.WriteLine(ID);
        }
    }
```

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


## 注释 (整合)

``` cs

1.HTML注释：

  <!-- this is a HTML comment -->

  如果是Csharp .cshtml页面注释还可以为
  @*<h2>Web Hosting</h2>*@

2.XML注释：

  <!-- this is a XML comment -->


3.CSS注释

  /* this is a css comment */

4.JavaScript 或 CSharp 或 PHP 注释：

  单行注释：//this is  a JavaScript comment

  多行注释：/* this is a JavaScript comment */

5.oracle &&  mysql && sqlserver：

  单行注释：-- this is  a oracle comment

  多行注释：/* this is a oracle comment */

7.JSP注释：

  <!-- this is a jsp comment -->

  <%-- this is a jsp comment --%>

7.Java注释：

  单行注释：// this is a java comment

  多行注释：/* this is a java comment */

  文档注释：/** this is a java comment */





```

## 多线程

[多线程跳转](./Csharp/多线程)

## 泛型（Generic）

[泛型](./Csharp/泛型)


## 折箱与装箱
主要就是值类型与引用类型转化
装箱：将值类型（如 int ，或自定义的值类型等）转换成 object 或者接口类型的一个过程。当 CLR 对值类型进行装箱时，会将该值包装为 System.Object 类型，再将包装后的对象存储在堆上。
拆箱就是从对象中提取对应的值类型的一个过程。
`装箱是隐式的；拆箱必定是显式的。`

　　与简单的赋值操作相比，装箱和拆箱都需要进行大量的数据计算。对值类型进行装箱时，CLR 必须重新分配一个新的对象。拆箱所需的强制转换也需要进行大量的计算，两者相比，仅仅是程度不高，并且也可能会出现类型转换发生的异常情形。
### 值类型 引用类型

1. 值类型：整型:Int；长整型:long；浮点型:float；字符型:char；布尔型:bool；枚举:enum；结构:struct；它们统一继承  System.ValueType。

2. 引用类型：数组，用户定义的类、接口、委托，object，字符串等。

3. 结构图

![下载](..\image\csharp01.png)

4. IL 代码

``` cs
.method private hidebysig static void  Main(string[] args) cil managed
{
  .entrypoint
  // 代码大小       19 (0x13)
  .maxstack  1
  .locals init ([0] int32 num,
           [1] object numObj,
           [2] int32 num2)
  IL_0000:  nop
  IL_0001:  ldc.i4.s   12   //加载一个值12放在堆栈中
  IL_0003:  stloc.0         //从堆栈顶弹出12并将其存储num中。
  IL_0004:  ldloc.0         //将num的值,加载到堆栈中。
  IL_0005:  box        [mscorlib]System.Int32  //装箱---将值类Int32转换为对象引用
  IL_000a:  stloc.1         //栈顶弹出当前的值 ,存储到numObj 中
  IL_000b:  ldloc.1         //将numObj 的值,加载到栈中
  IL_000c:  unbox.any  [mscorlib]System.Int32  //拆箱--将已装箱Int32的转换成未装箱形式。
  IL_0011:  stloc.2         //栈顶弹出当前的值 ,存储到num2中
  IL_0012:  ret             
} // end of method Program::Main

```
### 装箱
装箱就是值类型到 object 类型或者到该值类型所实现的接口类型所实现的一个隐式转换过程（可显式）。装箱的时候会在堆中自动创建一个对象实例，然后将该值复制到新对象内。
``` cs
 var i = 123;    //System.Int32

    //对 i 装箱（隐式）进对象 o
    object o = i;
```
![下载](..\image\csharp02.png)

如上图可知，装箱就是在堆上开出一个空间，然后把栈上的对像copy放去，然后将o对象在栈上的指针指向刚刚copy到堆上的对象
`注：装箱默认是隐式的，当然，你可以选择显式，但这并不是必须的。`
### 拆箱

　拆箱是从 object 类型到值类型，或从接口类型到实现该接口的值类型的显式转换的一个过程。

　　拆箱：检查对象实例，确保它是给定值类型的一个装箱值后，再将该值从实例复制到值类型变量中。
``` cs
    int i = 123;      // 值类型
    object o = i;     // 装箱
    int j = (int)o;   // 拆箱
    ```

![下载](..\image\csharp03.png)

## 关键字 new as is
[微软参考地址](https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/new-constraint)
## new 功能

1. 运算符
2. 修饰符
3. new约束

//当泛型类创建类型的新实例时，请将 new 约束应用于类型参数，如下面的示例所示：

``` cs

class ItemFactory<T> where T : new()
{
    public T GetNewItem()
    {
        return new T();
    }
}
//当与其他约束一起使用时，new() 约束必须最后指定：
public class ItemFactory2<T>
    where T : IComparable, new()
{
}
```
## const 与 readonly

**const(静态常量)：**编译器在编译的时候对常量进行初始化，并将常量替换成初始化的那个值

**readonly(动态常量)：**在运行时才确认，编译器在编译时，将其标记为只读常量。这个常量不必在初始化时就确定值，可以延迟在构造函数里面再进行初始化
注：如果在定义的时候就进行了初始化，但在构造函数里面还可以改。除这两处外，其它地方都不可以改。

readonly 可以针对任务类型进行修饰，所以它又分为两种情况

值类型：值类型是无法改变了。

引用类型：引用类型比较特殊，只读限定了常量的引用指针不可变，但是可以改变对像的实例。

```cs
public class Student{
	public  int age=10;
}
public class Program{
	private readonly Student st=new Student();
	void Main(){
    	st.age=100;//这个不会报错，只是调整了实例内部的值，但对只讯的指针没有影响
           st = new StudentA();//这样就会报错，重新对st指针做了新的指向
    }

}
```

## operator(关键字)

说明： 用来重载运算符

```cs

```

## 属性

### Propety 属性

```cs
public class Student
    {
        private string name;//字段
        public string Name { get; set; }//property 属性
    }
```
### Attribute 特性

```cs
 [DataContract]//attribute 特性
 public class Student
{
    [DataMember]//attribute 特性
     public string Name{ get; set; }
}
```

#### 自定义

自定义特性必须继承System.Attribute

```CS
//定义
public  class RemarkAttribute : Attribute
    {
        public string enum_Remark;
        public RemarkAttribute(string remark)//也可以不用带参数的
        {
            this.enum_Remark = remark;
        }
        public RemarkAttribute()
        {
        }
    }
//使用：
	[Remark("学生类")]
    public class Student
    {

        public Student()
        {
        }

        [Remark]
        public string Name { get; set; }
        [Remark]
        public void GetName() {

        }
    }
```

```cs
//实战demo 通过特性获取备注的具体内容

//1.自定义特性类
 public  class RemarkAttribute : Attribute
    {
        public string enum_Remark;
        public RemarkAttribute(string remark)
        {
            this.enum_Remark = remark;
        }

        public string GetRemark()
        {
            return enum_Remark;
        }
    }
  public static class Extend {
        public static string GetRemark(this Enum evalue)
        {
            try
            {
                Type type = evalue.GetType();
                System.Reflection.FieldInfo filed = type.GetField(evalue.ToString());
                if (filed.IsDefined(typeof(RemarkAttribute),true))
                {
                   //RemarkAttribute remark = (RemarkAttribute)filed.GetCustomAttributes(typeof(RemarkAttribute), true);
                    return ((RemarkAttribute)filed.GetCustomAttributes(true)[0]).GetRemark();
                }
                return "";
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return "";
        }
    }

//2.使用特性
 public enum Status {
        [Remark("正常")]
        Normal=0,
        [Remark("非正常")]
        not =1
    }

//3.反射特性信息
   var remark = Status.Normal.GetRemark();
//4.输出：正常
```

## 关健字
`new`约束指定泛型类声明中的任何类型实参都必须有公共的无形参构造函数。 若要使用 new 约束，则该类型不能为抽象类型。

## 访问级别

**private：**是完全私有的，只有在类自己里面可以调用，在类的外部和子类都不能调用，子类也不能继承父类的private的属性和方法。

**protected：**虽然可以被外界看到，但外界却不能调用，只有自己及自己的子类可以调用（protected的属性和方法都可以被子类所继承和调用）。

**private和protected的共同点**：外部都不可以访问。

**private和protected的不同点**：在同一类中可视为一样，但在继承中就不同了，private在派生类中不可以被访问，而protected可以。

**public：**对任何类和成员都完全公开，无限制访问。

**internal：**同一应用程序集内部（在VS.NET中的一个项目中，这里的项目是指单独的项目，不是整个解决方案，也不是命名空间，是指看下图）可以访问。

![下载](..\image\csharp05.png)


**public和internal的区别：**public的成员可以跨程序集，但internal不能，同一程序集中具有相同的效果。

**protected internal：**只能在同一应用程序集内本类、派生类访问。

