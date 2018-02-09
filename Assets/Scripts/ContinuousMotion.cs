using UnityEngine;
using System.Collections;

public class ContinuousMotion : MonoBehaviour {

	// Use this for initialization
	public float vel;
	public Transform limit;

	void Start () {
		this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(vel,0);
	}
	
	// Update is called once per frame
	void Update () {
		if(limit!=null){
			if(transform.position.x >= limit.position.x){
				this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				transform.position = limit.position;
			}
		}
	
	}
}
