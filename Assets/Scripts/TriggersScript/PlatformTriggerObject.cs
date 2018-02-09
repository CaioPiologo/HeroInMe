using UnityEngine;
using System.Collections;

public class PlatformTriggerObject : MonoBehaviour {

	public GameObject platform;
	public Vector3 finalPosition;
	public iTween.LoopType loopType;

	private Vector3 startPosition;

	void Start(){
		startPosition = platform.transform.position;
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Obstacle")){
			MoveThePlatform();
		}
	}

	public void MoveThePlatform(){
		iTween.StopByName("firstPlatform");
		platform.GetComponent<PlataformController>().MovePlatform(finalPosition, loopType);
	}
}
