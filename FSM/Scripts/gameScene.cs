using UnityEngine;
using System.Collections;

public class gameScene : MonoBehaviour {
	//登录场景  
	public GameObject sence1;  
	//注册场景  
	public GameObject sence2;  
	//Loading场景  
	public GameObject sence3;  
	//注册场景中账号的输入 
	public UIInput r_name;  
	//注册场景中第一次密码的输入  
	public UIInput r_pwd;  
	//注册场景中第二次密码的输入  
	public UIInput r_pwdok;  

	//Use this for initialization  
	void Start () {  
		//显示场景1  
		sence1.SetActive(true);  
		//不显示场景2  
		sence2.SetActive(false);  
		//不显示Loading场景  
		sence3.SetActive(false);          
	} 

	//登录场景切换到注册场景  
	public void change() {  
		//不显示场景1  
		sence1.SetActive(false);  
		//显示场景2  
		sence2.SetActive(true);   
	}  

	//注册场景切换到登录场景  
	public void toChange() {  
		//显示场景1  
		sence1.SetActive(true);  
		//不显示场景2  
		sence2.SetActive(false);  
	} 










	//当点击运行（go）按钮的时候触发的事件  
	public void sendToServer() {  
		//Loading场景，防止用户误操作  
		sence3.SetActive(true);  
		//向服务器发送数据     
	}  
	public void r_name_input() {  
			//如果账户输入为空  
			if (r_name.value == null || r_name.value == "") {  
				print("请输入账号");  
				return;        
		    }  
			//如果两次密码输入的值不相同  
			if (r_pwd.value != r_pwdok.value) {  
				print("两次密码输入不相同");  
				return;        
		    }      
	} 
} 
