using UnityEngine;
using System.Collections;

public class FollowCameraScript : MonoBehaviour {

	public GameObject camera;
	public Vector3 offset;

	public void SetPosition(){
		Vector3 startingPos = new Vector3(camera.transform.position.x + offset.x, camera.transform.position.y, 0);
		transform.position = startingPos;
	}

	// Update is called once per frame
	void LateUpdate () {
		Vector3 newPos = new Vector3(camera.transform.position.x + offset.x, camera.transform.position.y, 0);
		transform.position = newPos;
	}
}
