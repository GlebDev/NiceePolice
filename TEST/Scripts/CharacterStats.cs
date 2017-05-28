using UnityEngine;
using System.Collections;



public class CharacterStats : MonoBehaviour {

	public int SumOfMoney;
	public int TotalSumOfPenalty;
	public float MaxEnergy;
	public float CurEnergy{
		get{ 
			return _curEnergy;
		}
		set{
			if (value < 0) {
				_curEnergy = 0;
			} else if (value > MaxEnergy) {
				_curEnergy = MaxEnergy;
			} else {
				_curEnergy = value;
			}
		}
	}

	private float _curEnergy;

	// Use this for initialization
	void Start () {
		CurEnergy = MaxEnergy;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void AddPenalty(int money){
		if (money > 0) {
			TotalSumOfPenalty += money;
		}
	}
	public void AddMoney(int money){
		if (money > 0) {
			SumOfMoney += money;
			GameObject.Find("Console").SendMessage ("AddConsoleLine","money +" + money.ToString());
		}
	}
}
