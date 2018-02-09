using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallingPlatformController : MonoBehaviour {

	private float x;
	private float y;
	private float z;
	private bool isFalling = false;
	// Use this for initialization
	void Start () {
		x = gameObject.transform.position.x;
		y = gameObject.transform.position.y;
		z = gameObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.CompareTag("Player") && !isFalling) {
			isFalling = true;
			Shaking ();
		}
	}

	void Shaking () {

		Hashtable config =  iTween.Hash("x", 0.3, 
		                                "y", 0,
		                                "time", 1,
		                                "delay", 0.5,
		                                "oncomplete", "Fall");

		iTween.ShakePosition(gameObject, config);

	}

	void Fall () {

		Hashtable config =  iTween.Hash("x", x, 
		                                "y", y - 200, 
		                                "time", 3,
		                                "delay", 0,
		                                "easetype", iTween.EaseType.easeInCubic,
		                                "looptype", iTween.LoopType.none,
		                                "oncomplete", "BackToInitialPos");
		
		iTween.MoveTo(gameObject, config);
	}

	void BackToInitialPos () {

		isFalling = false;
//
//		Hashtable config = iTween.Hash("alpha", 0,
//		                               "time", 1,
//		                               "delay", 0);
//
//		iTween.ColorFrom(gameObject, config);
//
		gameObject.transform.position = new Vector3(x, y, z);

		Hashtable config = iTween.Hash("x", 0,
		                               "y", 0,
		                               "time", 1,
		                               "easetype", iTween.EaseType.easeInCubic);
		iTween.ScaleFrom(gameObject, config);

//		Hashtable config =  iTween.Hash("x", x, 
//		                                "y", y, 
//		                                "time", 3,
//		                                "delay", 0,
//		                                "easetype", iTween.EaseType.easeInOutCubic,
//		                                "looptype", iTween.LoopType.none);
//		
//		iTween.MoveTo(gameObject, config);
	}
}
