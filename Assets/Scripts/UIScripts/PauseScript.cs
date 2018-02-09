using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

	public string mainMenu;

	public bool isMuted = false;

	public GameObject pauseCanvas;

	public GameObject tvImage;

	public GameObject playerCanvas;

	public GameObject muteButton;
	public Sprite muteSprite;
	public Sprite unMuteSprite;
	

	public void ResumeGame(){
		tvImage.GetComponent<Animator>().SetTrigger("fade");
	}

	public void PauseGame(){
		pauseCanvas.SetActive(true);
		if(AudioListener.volume == 0f){
			isMuted = true;
			muteButton.GetComponent<Image>().sprite = unMuteSprite;
			muteButton.GetComponentInChildren<Text>().text = "Unmute";
		}
		playerCanvas.SetActive(false);
		Time.timeScale = 0f;
		tvImage.GetComponent<Animator>().SetTrigger("fade");
	}

	public void Quit(){
		Time.timeScale = 1f;
		iTween.Stop();
		Application.LoadLevel(mainMenu);
	}

	public void Mute(){
		isMuted = !isMuted;


		if(isMuted){
			AudioListener.volume = 0f;
			muteButton.GetComponent<Image>().sprite = unMuteSprite;
			muteButton.GetComponentInChildren<Text>().text = "Unmute";
		} else {
			AudioListener.volume = 1f;
			muteButton.GetComponent<Image>().sprite = muteSprite;
			muteButton.GetComponentInChildren<Text>().text = "Mute";
		}
	}


}
