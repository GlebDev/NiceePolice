using UnityEngine;
using System.Collections;

public class PropellingWeapon : MonoBehaviour {

	public int damage = 10;
	public AudioClip KickSound;
	public Transform BloodPrefab,DustPrefab;

	private bool IsKicked=false;
	private Quaternion HitRotation;

	void OnCollisionEnter(Collision other){
		if(!IsKicked && other.transform.GetComponent<Collider>() && !other.transform.CompareTag("Player") && !other.transform.GetComponent<Collider>().isTrigger){
			if (other.transform.GetComponent<InputDamage> ()) {
				other.transform.GetComponent<InputDamage> ().Hit (damage);

				HitRotation = Quaternion.FromToRotation (Vector3.up, other.contacts [0].normal);
				Instantiate (BloodPrefab, other.contacts [0].point, HitRotation, other.transform);
				
				GetComponent<AudioSource> ().PlayOneShot (KickSound);
				if (other.transform.root.GetComponent<HomelessAI> ()) {
					other.transform.root.GetComponent<HomelessAI> ().IsKick = true;
				}
			} else {
				HitRotation = Quaternion.FromToRotation (Vector3.up, other.contacts [0].normal);
				Instantiate (DustPrefab, other.contacts [0].point, HitRotation);
			}

		}
		IsKicked = true;
	}
}
