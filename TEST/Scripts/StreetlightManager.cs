using UnityEngine;
using System.Collections;

public class StreetlightManager : MonoBehaviour {

	[SerializeField] private Light[] StreetLightArr;
	public TimeFlow TimeManager;

	private float RndNum;

	// Use this for initialization
	void Start () {
		TimeManager.onDay += TurnOffTheLight;
		TimeManager.onNight += TurnOnTheLight;
		//TurnOnTheLight ();
	}



	void TurnOnTheLight (){
		Debug.Log ("on");
		foreach (Light CurLight in StreetLightArr) {
			CurLight.enabled = true;
			RndNum = Random.Range (0f, 1f);
			CurLight.GetComponent<Animator>().Play("StreetLightFlashing",0,RndNum);
		}
	}

	void TurnOffTheLight (){
		foreach (Light CurLight in StreetLightArr) {
			CurLight.enabled = false;
		}
	}
}
