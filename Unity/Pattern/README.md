# C_Sharp-Framework/Unity/Pattern #
基础设计模式及功能类

***

### Singleton 单例接口 ###
**Unity/Pattern/Singleton/**:`单例基类`

 - **BaseISingleton** - ***非Unity** 单例接口，直接继承此类，即可使用单例*
 - **BaseUnitySingleton** - ***Unity** 单例接口，已继承MonoBehaviour,实现组件化单例，组件默认隐藏，默认销毁、程序退出时清除，如果跨场景存在，需在子类实现DontDestroyOnLoad*

### 观察者模式 ###
**Unity/Pattern/Subscribe/**:`观察者模式`

 - **IObserver** - *订阅者接口，实现 `Name` 和 `IUpdate`方法*
	 - **Prop 属性**
		 - Name:*订阅者Name，发布者将其以Key方式注册到字典*
	 - **Func 方法**
		 - IUpdate:*执行发布者广播*
 - **ISubject** - *发布者接口*
	 - **Prop 属性**
		 - ObserverPool:*订阅者对象字典访问器，需要继承类实现 private 字典对象*
	 - **Func 方法**
		 - AddObserver:*添加订阅者对象*
		 - RemoveObserver:*移除订阅者对象*
		 - Notify:*广播发生器*
 
