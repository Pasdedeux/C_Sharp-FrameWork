using UnityEngine;
using System.Collections;

public class SetAct : MonoBehaviour {

	public GameObject xuankuang;

	public void App1(){
		xuankuang.SetActive (true);
	}

	public void App2(){
		xuankuang.SetActive (false);
	}
}
