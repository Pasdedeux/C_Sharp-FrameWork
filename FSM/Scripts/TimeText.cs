using UnityEngine;
using System.Collections;
using System;
public class TimeText : MonoBehaviour {

	public UILabel timerLabel; 
	private string timerText; 
	private float temp; 

	void Update () 
	{  
		temp += Time.deltaTime; 
		TimeSpan timeSpan = TimeSpan.FromSeconds(temp); 

		timerText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); 
		timerLabel.text = timerText; 

	}

}
