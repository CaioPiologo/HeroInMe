using UnityEngine;
using System.Collections;

public class EndLevelScript : MonoBehaviour {

	public float timeToCount = 0f;
	public UnityEngine.UI.Text textBox;
	public GameObject player;
	public GameObject levelCompleteCanvas;
	public GameObject playerCanvas;

	private int coins;

	void OnTriggerEnter2D(Collider2D col){
		EndLevelScreen();
	}

	public void EndLevelScreen(){
		coins = player.GetComponent<Player>().coinsInLevel;
		player.GetComponent<Player>().Pause();
		playerCanvas.SetActive(false);
		levelCompleteCanvas.SetActive(true);
		StartCoroutine("countCoins");
	}

	public IEnumerator countCoins () {
		for(int i = 0; i <= coins; i++){
			string text = "+" + i;
			textBox.text = text;
			yield return new WaitForSeconds(timeToCount);
		}
	}
}
