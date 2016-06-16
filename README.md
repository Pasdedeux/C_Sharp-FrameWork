# C_Sharp-Framework #
Use to build primary framework

***

### FSM 有限状态机 ###
**AI/**：`状态机实例`

- CubeAI - *直接添加至对象，入口执行文件*
- CubeIdle - *对象状态实现：站立*
- CubeRun - *对象状态实现：跑动*

**FSM/**：`状态机框架`

- **FSMMachine** - *状态机，主要属性及功能：*
	- **Prop 属性**
		- owner:*当前状态机的持有对象*
		-  currentState:*当前状态*
		-  previousState:*上一个状态*
		-  globalState:*全局状态*
	- **Func 方法**
		- SetCurrentState:*设置当前状态*
		- GetCurrentState:*获取当前状态*
		- FSMUpdate:*外部调用执行状态*
		- ChangeState:*改变状态* 
- **FSMState** - *状态基类*
	- **Prop 属性**
		- Entity_type:*当前状态机持有者，用于调用其(上层状态机的持有者)所拥有的方法*
	- **Func 方法**
		- virtual Enter:*进入状态的虚方法*
		- virtual Excute:*执行状态的虚方法*
		- virtual Exit:*退出状态的虚方法*
	
### ItemPool 对象池 ###
 - **ItemPool** - *对象池对象*
 - **ItemPoolManager** - *对象池管理*
 - **ItemPoolTime** - *带有效时间的对象池对象*

### Singleton 单例接口 ###
**Unity/Singleton/**:`单例基类`

 - **BaseISingleton** - ***非Unity** 单例接口，直接继承此类，即可使用单例*
 - **BaseUnitySingleton** - ***Unity** 单例接口，已继承MonoBehaviour,实现组件化单例，组件默认隐藏，默认销毁、程序退出时清除，如果跨场景存在，需在子类实现DontDestroyOnLoad*

### 观察者模式 ###
**Unity/Subscribe/**:`观察者模式`

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
 
