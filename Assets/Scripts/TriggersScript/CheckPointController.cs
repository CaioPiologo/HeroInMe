using UnityEngine;
using System.Collections;

public class CheckPointController : MonoBehaviour {

	private bool alreadyActivated = false;
	private AudioSource audioSource;

	public GameObject effectGameObject;
	public bool playSound = true;
	public AudioClip audio;
	public AudioClip audio2;

	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag("Player") && !alreadyActivated) {
			Player.RefreshCheckPointPos(gameObject.transform.position);
			alreadyActivated = true;
			if(effectGameObject){
				effectGameObject.SetActive(true);
			}

			if(playSound && audio && audio2) {
				audioSource.volume = 0.7f;
				audioSource.PlayOneShot(audio);
				audioSource.volume = 1.0f;
				audioSource.PlayOneShot(audio2);
			}
		}

	}
}
