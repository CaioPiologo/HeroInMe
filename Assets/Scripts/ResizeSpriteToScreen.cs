using UnityEngine;
using System.Collections;

public class ResizeSpriteToScreen : MonoBehaviour {

	void Start(){
		ResizeSpriteToScreenFunction();
		this.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,0);
		this.transform.parent = Camera.main.transform;
	}

	public void ResizeSpriteToScreenFunction() {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (sr == null){ 
			return;
		}

		float worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		float newLocalScale = (worldScreenWidth / sr.sprite.bounds.size.x);
		transform.localScale = new Vector3(newLocalScale, newLocalScale, 1);
	}

	public void SetMyselfActive(){
		this.gameObject.SetActive(true);
	}

	public void SetMyselfInactive(){
		this.gameObject.SetActive(false);
	}
}
