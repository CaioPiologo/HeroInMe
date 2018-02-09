using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	public UnityEngine.UI.Text textBox;

	public void UpdateCoinsUI(int coins){
		string text = "x" + coins;

		textBox.text = text;
	}
}
