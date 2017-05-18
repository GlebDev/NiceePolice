using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	[HideInInspector]public int remuneration;
	public int MaxHealth,CurHealth;
	public int dificulty;
	public ObjectHealth EnemyHealthScript;

	// Use this for initialization
	void Start () {
		EnemyHealthScript.MaxHealth = MaxHealth;
		EnemyHealthScript.CurHealth = CurHealth;
		remuneration = 10 * dificulty;

	}

	// Update is called once per frame
	void Update () {
	}
}
