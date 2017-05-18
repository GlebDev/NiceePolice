using UnityEngine;
using System.Collections;

public class MeleeAttackScript : MonoBehaviour {

	public int force=200;
	public AudioClip KickSound;
	public Transform BloodPrefab;

	private float CurKickTime,RangeOfAttack;
	private int damage;
	private float AttackSpeed;
	private Vector3 Direction;
	private bool AttackEnabled;
	private RaycastHit Hit;
	private Quaternion HitRotation;

	// Use this for initialization
	void Start () {
		damage = transform.GetComponent<MeleeWeapon> ().damage;
		AttackSpeed = transform.GetComponent<MeleeWeapon> ().AttackSpeed <= 0 ? 1 : transform.GetComponent<MeleeWeapon> ().AttackSpeed; 
		RangeOfAttack = transform.GetComponent<MeleeWeapon> ().RangeOfAttack;
	}
	
	// Update is called once per frame
	void Update () {
		if (CurKickTime < (60f/AttackSpeed)) {
			CurKickTime += Time.deltaTime;
		}else if (Input.GetMouseButtonDown(0) ) {
			CurKickTime = 0;
			GetComponent<Animator>().SetTrigger ("kick");

		}

		if (AttackEnabled) {
			Attack (damage);
		}

	}

	public void StartAttack(){
		AttackEnabled = true;
	}

	public void EndAttack(){
		AttackEnabled = false;
	}

	private void Attack(int _dmg){
		Direction = Camera.main.transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast (Camera.main.transform.position, Direction, out Hit, RangeOfAttack) && 
			!Hit.transform.GetComponent<Collider>().isTrigger && 
			!Hit.transform.CompareTag("Player")) {
			AttackEnabled = false;
			if (Hit.transform.GetComponent<Rigidbody> ()) {
				Hit.transform.GetComponent<Rigidbody> ().AddForceAtPosition (Direction * force, Hit.point); 
			}

			if (Hit.transform.GetComponent<InputDamage> ()) {
				Hit.transform.GetComponent<InputDamage> ().Hit (_dmg);
				GetComponent<AudioSource> ().PlayOneShot (KickSound);
				HitRotation = Quaternion.FromToRotation (Vector3.up, Hit.normal);
				Instantiate (BloodPrefab, Hit.point, HitRotation, Hit.transform);

				if (Hit.transform.root.GetComponent<HomelessAI> ()) {
					Hit.transform.root.GetComponent<HomelessAI> ().IsKick = true;
				}

			} 
		}
	}
}
