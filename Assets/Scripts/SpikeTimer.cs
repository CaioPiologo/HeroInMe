using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikeTimer : MonoBehaviour {

	public enum CycleType{
		constant,
		wave,
		negativeWave
	};
	public GameObject[] spikes;
	public float cycleTime;
	public CycleType cycleType; //0 = all at a time; 1 = wave; 2 = negative wave

	private float spikeTime;
	private float delay;
	private int spike;
	private int reverse = 1; //1 = up, -1 = down


	// Use this for initialization
	void Start () {
		spikeTime = cycleTime;
		delay = cycleTime * (spikes.Length*2);
		switch(cycleType){
		case CycleType.constant:
			for(int i = 0; i < spikes.Length; i++){
				MoveSpikeInLoop(i);
			}
			break;
		case CycleType.wave:
			StartCoroutine("timer");
			break;
		case CycleType.negativeWave:
			reverse = -1;
			StartCoroutine("timer");
			break;
		}
	}

	void MoveSpike (int i) {
		float x = spikes[i].transform.position.x;
		float y = spikes[i].transform.position.y;
		float width = spikes[i].GetComponent<SpriteRenderer>().bounds.size.x;
		float height = spikes[i].GetComponent<SpriteRenderer>().bounds.size.y * reverse;

		Hashtable config =  iTween.Hash("x", x, 
			"y", y + height, 
			"time", cycleTime,
			"easetype", iTween.EaseType.easeInOutElastic,
			"looptype", iTween.LoopType.none);
		

		iTween.MoveTo(spikes[i], config);

	}

	void UnMoveSpike(int i){
		float x = spikes[i].transform.position.x;
		float y = spikes[i].transform.position.y;
		float width = spikes[i].GetComponent<SpriteRenderer>().bounds.size.x;
		float height = spikes[i].GetComponent<SpriteRenderer>().bounds.size.y * reverse;

		Hashtable config =  iTween.Hash("x", x, 
			"y", y - height, 
			"time", delay,
			"delay", 0f,
			"easetype", iTween.EaseType.easeInOutElastic,
			"looptype", iTween.LoopType.none);
		
		iTween.MoveTo(spikes[i], config);
	}

	void MoveSpikeInLoop (int i) {
		float x = spikes[i].transform.position.x;
		float y = spikes[i].transform.position.y;
		float width = spikes[i].GetComponent<SpriteRenderer>().bounds.size.x;
		float height = spikes[i].GetComponent<SpriteRenderer>().bounds.size.y * reverse;

		Hashtable config =  iTween.Hash("x", x, 
			"y", y + height, 
			"time", cycleTime*(spikes.Length+1),
			"delay", 0f,
			"easetype", iTween.EaseType.easeInOutElastic,
			"looptype", iTween.LoopType.pingPong);


		iTween.MoveTo(spikes[i], config);

	}

	IEnumerator timer(){
		while(true){
			yield return new WaitForSeconds(cycleTime);
			MoveSpike(spike);
//			yield return new WaitForSeconds(cycleTime);
//			UnMoveSpike(spike);
			spike++;
			if(spike >= spikes.Length){
				spike = 0;
				reverse = reverse *-1;
			}
		}
	}

//	IEnumerator timer(){
//		spike = 0;
//		while(spike < spikes.Length){
//			yield return new WaitForSeconds(cycleTime);
//			MoveSpikeInLoop(spike);
//			spike++;
//		}
//	}

}
