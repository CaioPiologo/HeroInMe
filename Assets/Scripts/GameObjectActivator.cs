using UnityEngine;
using System.Collections;

public class GameObjectActivator : MonoBehaviour {

	public GameObject[] target;
	public bool activate = false;
	public bool shouldToggle = false;
	public bool useTrigger = true;

	public void ToggleGameObject(){
		for(int i = 0; i < target.Length; i++){
			target[i].SetActive(!target[i].gameObject.activeSelf);
		}
	}

	public void ActivateGameObject(){
		for(int i = 0; i < target.Length; i++){
			target[i].SetActive(true);
		}
	}

	public void DeactivateGameObject(){
		for(int i = 0; i < target.Length; i++){
			target[i].SetActive(false);
		}
	}

	public void Execute(){
		if(shouldToggle){
			ToggleGameObject();
		} else if(activate){
			ActivateGameObject();
		} else {
			DeactivateGameObject();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("Player")){
			if(useTrigger){
				Execute();
			}
		}
	}
}
