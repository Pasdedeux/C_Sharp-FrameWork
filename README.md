# C_Sharp-FrameWork
Use to build primary framework

***

### FSM 有限状态机
**AI/**：`状态机实例`

- CubeAI - *直接添加至对象，入口执行文件*
- CubeIdle - *对象状态实现：站立*
- CubeRun - *对象状态实现：跑动*

**FSM/**：`状态机框架`

- **FSMMachine** - *状态机，主要属性及功能：*
	- **prop 属性**
		- owner:*当前状态机的持有对象*
		-  currentState:*当前状态*
		-  previousState:*上一个状态*
		-  globalState:*全局状态*
	- **Func 方法**
		- SetCurrentState:*设置当前状态*
		- GetCurrentState:*获取当前状态*
		- FSMUpdate:*外部调用执行状态*
		- ChangeState:*改变状态* 
		


