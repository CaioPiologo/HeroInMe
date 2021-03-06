﻿using UnityEngine;
using System.Collections;

public class DeathEffector : MonoBehaviour {

	public float explosionTime = 0.2f;
	public float maxRotation = 500;
	public Rigidbody2D headRB2D;
	public Rigidbody2D frontHandRB2D;
	public Rigidbody2D backHandRB2D;
	public Rigidbody2D frontFootRB2D;
	public Rigidbody2D backFootRB2D;

	// Use this for initialization
	void Start () {
		Invoke("Deactivate", explosionTime);

		PartsRotation();

		if (transform.parent.localScale.x < 0) {
			transform.parent.localScale = new Vector3(0.5f, 0.5f, 1f);
			headRB2D.gameObject.transform.localScale = new Vector3(-1, 1, 1);
			frontHandRB2D.gameObject.transform.localScale = new Vector3(-1, 1, 1);
			backHandRB2D.gameObject.transform.localScale = new Vector3(-1, 1, 1);
			frontFootRB2D.gameObject.transform.localScale = new Vector3(-1, 1, 1);
			backFootRB2D.gameObject.transform.localScale = new Vector3(-1, 1, 1);

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Deactivate () {
		gameObject.SetActive(false);
	}

	void PartsRotation() {
		float rand = Random.Range(-maxRotation, maxRotation);
		headRB2D.angularVelocity = rand;

		rand = Random.Range(-maxRotation, maxRotation);
		frontHandRB2D.angularVelocity = rand;

		rand = Random.Range(-maxRotation, maxRotation);
		backHandRB2D.angularVelocity = rand;

		rand = Random.Range(-maxRotation, maxRotation);
		frontFootRB2D.angularVelocity = rand;

		rand = Random.Range(-maxRotation, maxRotation);
		backFootRB2D.angularVelocity = rand;
	}
}
