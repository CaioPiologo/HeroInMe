using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	public void Play(){
		audioSource.Play();
	}

	public void Pause(){
		audioSource.Pause();
	}
}
