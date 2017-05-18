using UnityEngine;
using System.Collections;

public class InputDamage : MonoBehaviour {
	public ObjectHealth HP;
	public float multipler;
	// Use this for initialization
	public void Hit(int damage){
		HP.ReduceHealth (Mathf.RoundToInt(damage * multipler));
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
