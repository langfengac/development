# C#基础



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

