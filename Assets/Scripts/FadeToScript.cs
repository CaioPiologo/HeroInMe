using UnityEngine;
using System.Collections;

public class FadeToScript : MonoBehaviour {

	public float alpha = 0;
	public float timeToFade = 1;

	void OnLevelWasLoaded(){
		Hashtable config =  iTween.Hash("alpha", alpha, 
			"time", timeToFade);


		iTween.FadeTo(this.gameObject, config);
	}
}
