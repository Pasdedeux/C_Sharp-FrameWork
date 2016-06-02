using UnityEngine;
using System.Collections;

public class Register : MonoBehaviour {

	public Login login;
	//public GameObject tip2;
	public void OnRegisterButtonClick(){
		login.userName = login.username.text;
		login.passWord = login.password.text;
		login.tip.gameObject.SetActive (false);
		Debug.Log ("注册成功");
	}
}
