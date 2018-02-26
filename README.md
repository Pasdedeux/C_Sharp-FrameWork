# C_Sharp-Framework #
Use to build primary frameworks


### FSM 有限状态机 ###
---
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
---
 - **ItemPool** - *对象池对象*
 - **ItemPoolManager** - *对象池管理*
 - **ItemPoolTime** - *带有效时间的对象池对象*

### Singleton 单例接口 ###
 - **IUnitySingleton** - *Unity 单例接口，直接继承此类，替代Monobehavior*

### RaycastReflection 射线反射 ###
- **RaycastReflection** - *通过这个类可以由本物体发射一条射线，允许碰撞物体产生反射射线及路径*
 
### Unity 功能脚本 ###
---
**Unity/Pattern/**:*设计模式*

**Unity/Mono/**:*Monobehaviour功能代码*

**Unity/HighlightingSystem/**:*物体高亮组件：直接挂载于需要高亮的物体上，通过flashn/flashoff开关*

**Unity/Editor/**:*编辑器功能代码*
	
	AlignTool：根据多个物体碰撞盒子（目前仅限于BoxCoollider）轴向对齐

### WordsShield 屏蔽字功能
---
### GetBoxColliderWorldPosition 获取Box碰撞器所有碰撞点世界坐标
---


 
