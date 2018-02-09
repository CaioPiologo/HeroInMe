using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AlwaysStayUp : MonoBehaviour {

	public Transform toFollow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	[ExecuteInEditMode]
	void LateUpdate () {
		transform.position = toFollow.position;
	}
}
