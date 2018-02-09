using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour {
	public string nextLevelName;

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D col){
		LoadLevel();
	}

	public void LoadAnimation () {
		animator.SetTrigger("Change Level");

	}
	public void LoadContinueAnimation () {
		animator.SetTrigger("continue");

	}

	public void NewGame() {

		Player.startOnCheckPoint = false;
		PlayerPrefs.SetInt("Continue", 1);
		PlayerPrefs.SetFloat("CheckPointX", 0f);
		PlayerPrefs.SetFloat("CheckPointY", 0f);
		PlayerPrefs.SetFloat("CheckPointZ", 3f);
		PlayerPrefs.Save();

		Player.RefreshCheckPointPos(new Vector3(0f, 0f, 3f));
		Player.coins = 0;

		LoadLevel();
	}

	public void LoadLevel() {

		if (nextLevelName == "Cave1" && !Player.startOnCheckPoint) {
			PlayerPrefs.SetFloat("CheckPointX", 13.93f);
			PlayerPrefs.SetFloat("CheckPointY", 81.39f);
			PlayerPrefs.SetFloat("CheckPointZ", -0.0007114305f);

			Player.RefreshCheckPointPos(new Vector3(13.93f, 81.39f, -0.0007114305f));
		} else  if (nextLevelName == "credits" && Player.currentLevel == "Cave1") {
			Player.startOnCheckPoint = false;
			PlayerPrefs.SetInt("Continue", 0);
		}

		Application.LoadLevel(nextLevelName);
		//Player.currentLevel = NextLevelName;
		PlayerPrefs.SetString("CurrentLevel", nextLevelName);
	}

	public void LoadGame() {

		string levelName = PlayerPrefs.GetString("CurrentLevel");

		if(levelName != null) {
			nextLevelName = levelName;
		}

		float checkPointX = PlayerPrefs.GetFloat("CheckPointX");
		float checkPointY = PlayerPrefs.GetFloat("CheckPointY");
		float checkPointZ = PlayerPrefs.GetFloat("CheckPointZ");

		Player.coins = PlayerPrefs.GetInt("Coins");
		Player.currentLevel = nextLevelName;
		Player.RefreshCheckPointPos(new Vector3(checkPointX, checkPointY, checkPointZ));

//		Player.coins = coins;
		if(nextLevelName != "CaioScene64"){
			Player.startOnCheckPoint = true;	
		}
		LoadLevel();
	}

	public void SimpleLoad(string name) {
		Application.LoadLevel(name);
	}
}
