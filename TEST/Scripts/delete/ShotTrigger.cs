using UnityEngine;
using System.Collections;

public class ShotTrigger : MonoBehaviour {

	public Collider trigger;
	public int force=100;
	public AudioClip KickSound;

	private int damage=0;
	 


	// Use this for initialization
	void Start () {
		damage = transform.GetComponent<MeleeWeapon> ().damage;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other.transform.name);
		if(other.transform.GetComponent<Collider>() && !other.transform.CompareTag("Player") && !other.transform.GetComponent<Collider>().isTrigger){
			
			trigger.enabled= false;
			GetComponent<AudioSource> ().PlayOneShot (KickSound);

			if (other.transform.GetComponent<Rigidbody> ()) {
				other.transform.GetComponent<Rigidbody> ().AddForceAtPosition (Camera.main.transform.forward* force,other.transform.position); 

			}

			if (other.transform.GetComponent<InputDamage> ()) {
				other.transform.GetComponent<InputDamage> ().Hit( Mathf.RoundToInt(damage * GameObject.Find ("GameSettings").GetComponent<GameSettings> ().MeleeWeaponDamageMultipler));

				if (other.transform.root.GetComponent<HomelessAI> ()) {
					other.transform.root.GetComponent<HomelessAI> ().IsKick = true;
				}

			}

		}
	}
}
