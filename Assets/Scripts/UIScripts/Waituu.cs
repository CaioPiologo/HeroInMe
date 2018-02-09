using UnityEngine;
using System.Collections;

public class Waituu : MonoBehaviour {

	public GameObject playerCanvas;
	public GameObject pauseCanvas;

	public void waituu(){
		Time.timeScale = 1;
		pauseCanvas.SetActive(false);
		playerCanvas.SetActive(true);
	}
}
