using UnityEngine;
using System.Collections;

public class SmoothFollow2D : MonoBehaviour {

	public float dampTime = 0.15f;
	public float levelMaxX;
	public float levelMinX;
	public float levelMaxY;
	public float levelMinY;
	public Transform target;

	private Vector3 velocity = Vector3.zero;
	private Camera camera;

	void Start () {
		camera = GetComponent<Camera>();
	}

	void Update () {
		if (target) {
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
//			transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);
		}

		if(transform.position.x > levelMaxX){
			Vector3 pos = new Vector3(levelMaxX, transform.position.y, transform.position.z);
			transform.position = pos;
		} else if(transform.position.x < levelMinX){
			Vector3 pos = new Vector3(levelMinX, transform.position.y, transform.position.z);
			transform.position = pos;
		}

		if(transform.position.y > levelMaxY){
			Vector3 pos = new Vector3(transform.position.x, levelMaxY, transform.position.z);
			transform.position = pos;
		} else if(transform.position.y < levelMinY){
			Vector3 pos = new Vector3(transform.position.x, levelMinY, transform.position.z);
			transform.position = pos;
		}
	}
}
