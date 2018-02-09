using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContinueScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("Continue") == 1) {
			this.GetComponent<Button>().interactable = true;
		} else {
			this.GetComponent<Button>().interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
