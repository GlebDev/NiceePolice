using UnityEngine;
using System.Collections;

public class WatchTrigger : MonoBehaviour {

	public HomelessAI AI;

	void OnTriggerStay(Collider other){
		if (AI.IslookAround && other.CompareTag("Player")) {
			AI.HaveSeen = true;
		}
	}
}
