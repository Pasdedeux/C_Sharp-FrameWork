using UnityEngine;
using System.Collections;	
using UnityEngine.UI; //使用UI控件，需要加入如下命名空间 
using UnityEngine.SceneManagement;

	public class Login : MonoBehaviour {  
		//接收各控件引用 
		private bool isRegister = false;
		public UILabel username;     
		public UILabel password;
		public string userName;
		public string passWord;
		public GameObject tip;  
		public void OnLoginButtonClick(){ 
			string username = this.username.text;
			string password = this.password.text; 
			if (username == userName && password == passWord) { 
			//登录成功之后，跳转到游戏界面             
			SceneManager.LoadSceneAsync ("select");
			tip.gameObject.SetActive (false);
			isRegister = true;
			} else if (username != userName || password != passWord) { 
			tip.gameObject.SetActive (true);
			}
		}	  
}
