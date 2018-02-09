using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlataformController : MonoBehaviour {

	public Vector3 finalPosition;
	public float speed = 2;
	public float delay = 1;
	public bool isTriggered;

	// Use this for initialization
	void Start(){
		if(isTriggered == false){
			MovePlatform(finalPosition, iTween.LoopType.pingPong);
		}
	}

	public void MovePlatform (Vector3 position, iTween.LoopType loopType) {
		float x = position.x;
		float y = position.y;


		Hashtable config =  iTween.Hash("x", x, 
		         						"y", y, 
		         						"speed", speed,
		          						"delay", delay,
		                                "easetype", iTween.EaseType.easeInOutCubic,
										"looptype", loopType,
										"name", "firstPlatform");

		iTween.MoveTo(gameObject, config);

	}
}
