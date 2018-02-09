using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour {

	public GameObject projectile;
	public GameObject shootingPoint;
	public float frequency = 2;
	public float shotSpeed = 5;
	public float shotLifespan = 5;
	public bool left = true;
	public bool active = false;
	public AudioClip audioClip;

	private float localFrequency;
	private Vector2 direction;
	private Animator animator;
	private AudioSource audioSource;

	void Start(){
		localFrequency = frequency;
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();

		if(left){
			direction.x = -1;
		} else{
			direction.x = 1;
		}
		direction.y = 0;
	}

	void Update () {
		if(active){
			localFrequency -= Time.deltaTime;
			if(localFrequency <= 0){
				localFrequency = frequency;

				animator.SetTrigger("Shoot");
				GameObject clone = (GameObject) Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
				clone.GetComponent<EnemyProjectile>().lifeSpan = shotLifespan;
				Vector3 newScale = clone.transform.localScale;
				newScale.x *= direction.x;
				clone.transform.localScale = newScale;
				Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), clone.GetComponent<Collider2D>());
				Rigidbody2D shotRigidbody = clone.GetComponent<Rigidbody2D>();
				shotRigidbody.velocity = new Vector2(direction.x*shotSpeed, direction.y*shotSpeed);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("Player")){
			active = true;
		}
	}

	public void TakeDamage(){
		active = false;
	}

	public void PlayQuackAudio(){
		audioSource.priority = 128;
		audioSource.PlayOneShot(audioClip, 1f);
	}
}
