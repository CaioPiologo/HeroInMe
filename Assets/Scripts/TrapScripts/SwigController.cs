using UnityEngine;
using System.Collections;

public class SwigController : MonoBehaviour {

	public float amplitude;
	public float time;
	public float delay;
	public bool clockwise;
	public bool pingpong;
	
	// Use this for initialization
	void Start () {
		if (clockwise) {
			amplitude = -amplitude;
		}

		Rotate();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Rotate () {
		Hashtable config =  iTween.Hash("z", amplitude, 
		                                "time", time,
		                                "delay", delay,
		                                "easetype", iTween.EaseType.easeInOutQuint,
		                                "looptype", iTween.LoopType.none,
		                                "oncomplete", "Rotate");
		
		iTween.RotateAdd(gameObject, config);

		if(pingpong) {
			amplitude = -amplitude;
		}
	}
}
