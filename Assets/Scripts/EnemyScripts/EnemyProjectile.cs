using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	public float lifeSpan;
	public int damage;

	void Awake () {

	}

	// Update is called once per frame
	void Update () {
		lifeSpan -= Time.deltaTime;
		if(lifeSpan <= 0){
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		//Colisao com inimigo
		if (col.gameObject.CompareTag("Player")) {
			col.gameObject.GetComponent<Player>().TakeDamage(damage);
			Destroy(gameObject);
		} else if(col.gameObject.CompareTag("Destructable")){
			col.gameObject.GetComponent<DestructableController>().TakeDamage(damage);
			Destroy(gameObject);
		}else if(col.gameObject.CompareTag("ground") ||
			col.gameObject.CompareTag("Wall") ||
			col.gameObject.CompareTag("Platform") ||
			col.gameObject.CompareTag("Obstacle") ||
			col.gameObject.CompareTag("Projectile")){

			Destroy (gameObject);
		}
	}
}
