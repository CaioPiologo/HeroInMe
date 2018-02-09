using UnityEngine;
using System.Collections;

public class ScriptDoParanaue : MonoBehaviour {

	public GameObject sword;
	public GameObject swipeUpTutorial;
	public GameObject tapTutorial;
	public GameObject goSomewhereElseTutorial;
	public GameObject mainCamera;
	public GameObject canvas;
	public GameObject player;
	public GameObject platformTrigger;

	public void ActivateCanvas(){
		canvas.SetActive(true);
	}

	public void DeactivateCanvas(){
		canvas.SetActive(false);
	}

	public void DropSwordScene(){
		sword.SetActive(true);
	}

	public void StartSwipeUpAnimation(){
		Vector3 startingPos = new Vector3(mainCamera.transform.position.x + 9.1f, mainCamera.transform.position.y, 0);
		GameObject clone = (GameObject) Instantiate(swipeUpTutorial, startingPos, mainCamera.transform.rotation);
		clone.transform.parent = mainCamera.transform;
		clone.GetComponent<Animator>().SetBool("InTutorial", true);
	}

	public void StartTapAnimation () {
		GameObject tutorialAnimation = (GameObject) Instantiate(tapTutorial, mainCamera.transform.position, mainCamera.transform.rotation);
		tutorialAnimation.transform.parent = mainCamera.transform;
		tutorialAnimation.GetComponent<Animator>().SetBool("AttackTutorial", true);
		player.GetComponent<PlayerControls>().inTutorial = true;
	}

	public void GoSomewhereElseTutorial(){
		goSomewhereElseTutorial.SetActive(true);
	}

	public void TriggerPlayerRefuseMoveAnimation(){
		player.GetComponent<Animator>().SetBool("RefuseToMove", true);
		player.GetComponent<Player>().speed = 3.2f;
	}

	public void StopPlayerRefuseMoveAnimation(){
		player.GetComponent<Animator>().SetBool("RefuseToMove", false);
		player.GetComponent<Player>().speed = 5.2f;
	}

	public void SetPlatformActive(){
		platformTrigger.SetActive(true);
	}
}
