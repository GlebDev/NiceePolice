using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Runing : MonoBehaviour {

	public CharacterStats stats;
	public FirstPersonController FPC;
	public float EDifference,ESum;


	private InterfaceManager IM;
	private float OriginalRunSpeed;

	// Use this for initialization
	void Start () {
		IM = GameObject.Find ("Interface").transform.GetComponent<InterfaceManager> ();
		OriginalRunSpeed = FPC.m_RunSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.LeftShift)) {
			if (stats.CurEnergy > 0) {
				stats.CurEnergy -= EDifference;
				IM.SetEnergyProgress ( stats.CurEnergy/stats.MaxEnergy);
				FPC.m_RunSpeed = OriginalRunSpeed;
			} else {
				FPC.m_RunSpeed = FPC.m_WalkSpeed;
			}
		} else if(stats.CurEnergy != stats.MaxEnergy){
			if (stats.CurEnergy < stats.MaxEnergy) {
				stats.CurEnergy += ESum;
				IM.SetEnergyProgress ( stats.CurEnergy/stats.MaxEnergy);
			} else {
				stats.CurEnergy = stats.MaxEnergy;
				IM.SetEnergyProgress ( stats.CurEnergy/stats.MaxEnergy);
			}
		}

	}
}
