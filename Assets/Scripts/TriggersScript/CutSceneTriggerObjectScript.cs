using UnityEngine;
using System.Collections;

public class CutSceneTriggerObjectScript : MonoBehaviour {

	public CutSceneSystem cutSceneSys;
	public CutScene scene;
	
	private void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("Player")){
			cutSceneSys.playScene(scene);
			this.gameObject.SetActive(false);
		}
	}
}
