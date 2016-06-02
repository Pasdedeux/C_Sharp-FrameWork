using UnityEngine;
using System.Collections;
using System;
public class TimeText2 : MonoBehaviour {

	public UILabel timerLabel; 
	private string timerText; 
	private float temp; 

	void Update () 
	{  
		temp += Time.deltaTime; 
		TimeSpan timeSpan = TimeSpan.FromSeconds(temp); 
		int time = 40 - timeSpan.Seconds;
		timerText = string.Format("{0}",time); 
		timerLabel.text = timerText; 

	}
}
