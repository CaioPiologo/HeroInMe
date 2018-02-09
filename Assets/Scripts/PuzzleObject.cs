using UnityEngine;
using System.Collections;

public class PuzzleObject : MonoBehaviour {

	public int puzzleTime = 5;

	private Vector3 myPosition;
	private Quaternion myRotation;

	void Start(){
		myRotation = this.transform.rotation;
		myPosition = this.transform.position;
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("Bullet") || col.gameObject.CompareTag("Player")){
			StartCoroutine("timer");
		}
	}

	IEnumerator timer(){
		yield return new WaitForSeconds(puzzleTime);
		this.ReturnToPosition();
	}

	public void ReturnToPosition(){
		GameObject clone = (GameObject) Instantiate(this.gameObject, myPosition, myRotation);
		Destroy(gameObject);
	}
}
