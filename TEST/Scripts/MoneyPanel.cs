using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyPanel : MonoBehaviour {

	public CharacterStats _characterStats;

	// Use this for initialization
	void Start () {
		Redraw ();
	}

	public void Redraw(){
		transform.GetChild (0).GetComponent<Text> ().text = "MONEY: " + _characterStats.SumOfMoney.ToString ();
	}
	

}
