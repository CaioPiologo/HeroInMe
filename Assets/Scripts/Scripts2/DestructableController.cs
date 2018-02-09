using UnityEngine;
using System.Collections;

public class DestructableController : MonoBehaviour {

	public int hp;
	public int damageHP;
	public Sprite damagedSprite;
	public AudioClip[] hitSound;
	public AudioClip breakSound;
	public GameObject audioPlayer;

	private int currentHP;
	private AudioSource audioSource;
	private float volMax = 1f;
	private float volMin = 0.5f;

	void Start(){
		currentHP = hp;
		audioSource = audioPlayer.GetComponent<AudioSource>();
	}

	public void TakeDamage(int damage){
		currentHP -= damage;
		float vol = Random.Range (volMin, volMax);
		if(currentHP <= 0){
			audioSource.PlayOneShot(breakSound, 1f);
			gameObject.SetActive(false);
			GameObjectActivator activator = GetComponent<GameObjectActivator>();
			if(activator){
				activator.Execute();
			}
		} else if(currentHP <= damageHP){
			this.GetComponent<SpriteRenderer>().sprite = damagedSprite;
			audioSource.PlayOneShot(hitSound[0], vol);
		} else {
			audioSource.PlayOneShot(hitSound[1], vol);
		}
	}
}
