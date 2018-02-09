using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

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
		if (col.gameObject.CompareTag("Enemy")) {
			//TODO: Aplicar dano
			Destroy(gameObject);
		} else if(col.gameObject.CompareTag("Destructable")){
			col.gameObject.GetComponent<DestructableController>().TakeDamage(damage);
			Destroy(gameObject);
		}else if(col.gameObject.CompareTag("ground") ||
			col.gameObject.CompareTag("Wall") ||
			col.gameObject.CompareTag("Platform") ||
		         col.gameObject.CompareTag("Obstacle") ||
		         col.gameObject.CompareTag("DeathTrap") ||
		         col.gameObject.CompareTag("DeathTrigger") ||
		         col.gameObject.CompareTag("EnemyProjectile")) {
			StartCoroutine("deathTimer");
//			Destroy (gameObject);
		}
	}

	IEnumerator deathTimer(){
		yield return new WaitForSeconds(0.01f);
		Destroy(this.gameObject);
	}
}
