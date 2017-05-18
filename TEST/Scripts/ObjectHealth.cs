using UnityEngine;
using System.Collections;

public class ObjectHealth : MonoBehaviour {

	[HideInInspector] public int MaxHealth,CurHealth;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	public void ReduceHealth(int damage){
		if (CurHealth >= 0 && damage>0) {
			CurHealth -= damage ;
			GameObject.Find("Console").SendMessage ("AddConsoleLine","deal damage:" + damage.ToString());
		}
		if(CurHealth<=0){
			transform.root.GetComponent<HomelessAI> ().IsDead = true;
		}
	}
}
