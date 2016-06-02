using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Select : MonoBehaviour {

	public UITexture touxiang_diban;
	//public GameObject[] touxiangs;
	public GameObject touxiang;
	private GameObject xuankuang;
	private AudioSource heroAudio;
	private Image touxiangImage;

	void Start(){
		heroAudio = touxiang.GetComponent<AudioSource> ();
		touxiangImage = touxiang.GetComponent<Image> ();
	}

	public void App(){
		heroAudio.Play ();
		touxiang_diban.mainTexture = touxiangImage.mainTexture;
	}
}
