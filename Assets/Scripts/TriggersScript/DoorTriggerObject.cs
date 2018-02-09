using UnityEngine;
using System.Collections;

public class DoorTriggerObject : MonoBehaviour {

	public GameObject door;
	public Sprite changeSprite;

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("Player") ||
			col.gameObject.CompareTag("Obstacle")){
			if(door != null){
				door.SetActive(false);
			}
			GetComponent<SpriteRenderer>().sprite = changeSprite;
		}
	}

}
