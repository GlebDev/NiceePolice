using UnityEngine;
using System.Collections;

public class RayCastShooting : MonoBehaviour {

	public Transform _camera;
	public Transform WoodHit;
	public Transform Sparks;
	public Transform MuzzleShafts;
	public GameObject MuzzleLight;
	private float muzzlelightTime;
	private RaycastHit Hit;
	public int BullHoleLifeTime = 5;
	public float RateOfSpeed = 0.5f;
	private float _rateOfSpeed;
	public Transform Hands;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(RateOfSpeed >= _rateOfSpeed){
			_rateOfSpeed += Time.deltaTime;
		}
		if (0.1 >= muzzlelightTime) {
			muzzlelightTime += Time.deltaTime;
		} else {
			MuzzleLight.SetActive (false);
		}
		if (Input.GetMouseButtonDown (0) && _rateOfSpeed>RateOfSpeed && !Hands.GetComponent<Animator>().GetBool("IsRun")) {
			_rateOfSpeed = 0;
			muzzlelightTime = 0;
			Vector3 Direction = _camera.TransformDirection (Vector3.forward);
			MuzzleShafts.GetComponent<ParticleEmitter> ().emit = true;
			MuzzleLight.SetActive (true);
			if (Physics.Raycast (_camera.position, Direction, out Hit, 10000f)) {
				Quaternion HitRotation = Quaternion.FromToRotation (Vector3.up, Hit.normal);
				if (Hit.transform.GetComponent<Rigidbody> ()) {
					Hit.transform.GetComponent<Rigidbody> ().AddForceAtPosition (Direction * 100, Hit.point); 
				}
				if (Hit.collider.material.name == "Wood (Instance)") {
					//Bulet hole
					Transform WoodHitGO = Instantiate (WoodHit, Hit.point + (Hit.normal * 0.001f), HitRotation) as Transform;
					WoodHitGO.parent = Hit.transform;
					Destroy ((WoodHitGO as Transform).gameObject, BullHoleLifeTime);
					//Sparks
					Instantiate (Sparks, Hit.point + (Hit.normal * 0.001f), HitRotation);
				}
			}
		} else {
			MuzzleShafts.GetComponent<ParticleEmitter> ().emit = false;
		}
	}
}
