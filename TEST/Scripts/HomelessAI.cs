using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class HomelessAI : MonoBehaviour {

	public GameObject _player;
	public Transform target;
	public List<Transform> TargetPoints,EscapePoints;
	public Transform TargetObject;
	public float ActionTime=20;
	public float CurActionTime;
	public bool IsDead,IsKicked,IsAction,IsKick,IslookAround,HaveSeen;
	public AnimationClip LookAroundAnim;
	public int LookAroundResult;
	public Rigidbody[] Elements;


	private Transform TempTarget;
	private NavMeshAgent agent;
	private int RundNum, OldRundNum=-1;
	private Vector3 ThisRotation;
	private float CurLookAroundAnimTime;
	private bool ChangeTarget=true;


	// Use this for initialization
	void Start () {
		TargetPoints=GameObject.Find ("GameSettings").GetComponent<GameSettings>().TargetPoints;
		EscapePoints=GameObject.Find ("GameSettings").GetComponent<GameSettings>().EscapePoints;
		agent = GetComponent<NavMeshAgent> ();
		if (!_player) {
			_player = GameObject.Find ("FPSController");
		}
		RandomTargetPoints (TargetPoints);
		GoTo (TempTarget);

	}
	
	// Update is called once per frame
	void Update () {
		GoTo(TempTarget);
		if (Vector3.Distance (transform.position, target.position) < 0.1f) {
			if (LookAroundResult == 0) {
				LookAround ();
			}
			if(LookAroundResult== -1){  //Have not seen police
				Interact(TargetObject);

			}
			if (LookAroundResult == 1) { //Have seen police
				RandomTargetPoints (TargetPoints);
				LookAroundResult = 0;
			}
			if (target.CompareTag ("Escape")) {
				Destroy (gameObject);
			}

		} 
		if(IsKick && !IsKicked){
			if(!IsAction){
				if (_player.GetComponent<CharacterStats> ()) {
					_player.GetComponent<CharacterStats> ().AddPenalty(GameObject.Find ("GameSettings").GetComponent<GameSettings>().AmountPenalty);
				}
			}
			RunAway ();
			IsKick = false;
			IsKicked=true;
		}
		if(IsDead){
			Die ();
		}
	}



	
	void GoTo(Transform I){
		GetComponent<Animator> ().SetBool ("IsWalk", true);
		target = I;
		agent.SetDestination (target.position);
	}



	void RandomTargetPoints(List<Transform> Points) {
		if (Points.Count>0) {
			
			do {
				RundNum = Random.Range (0, Points.Count);
			} while(RundNum == OldRundNum && Points[RundNum].GetComponent<IsBeezy>().beezy==true);
			OldRundNum = RundNum;
			TargetObject = Points[RundNum];
			TargetObject.GetComponent<IsBeezy> ().beezy = true;
			TempTarget = TargetObject.GetChild(0);
		} else {
			TempTarget = _player.transform;
		}
	}
	void RunAway() {
		if (!IsAction) {
			TempTarget = EscapePoints [Random.Range (0, EscapePoints.Count)];
			GetComponent<NavMeshAgent> ().speed = 7;
			GetComponent<Animator> ().SetBool ("IsRun", true);
		} else {
			CurActionTime = ActionTime;
		}
	}

	void LookAround(){
		GetComponent<Animator> ().SetBool ("IsWalk", false);
		GetComponent<Animator> ().SetBool ("IsLookAround", true);
		IslookAround = true;
		if( CurLookAroundAnimTime <= LookAroundAnim.length){
			CurLookAroundAnimTime += Time.deltaTime;
		}
		if (CurLookAroundAnimTime > LookAroundAnim.length) {
			GetComponent<Animator> ().SetBool ("IsWalk", true);
			GetComponent<Animator> ().SetBool ("IsLookAround", false);

			if (HaveSeen) {
				LookAroundResult = 1;
			}else{
				LookAroundResult = -1;
			}
			CurLookAroundAnimTime = 0;
			HaveSeen = false;
		}
	}

	void Interact(Transform Target) {
		IsAction = true;
		transform.LookAt(TargetObject);
		ThisRotation = transform.localEulerAngles;
		transform.localEulerAngles=new Vector3(0,ThisRotation.y,0);
		if (CurActionTime <= ActionTime+5f) {
			CurActionTime += Time.deltaTime;
		}
		if (HaveSeen) {
			CurActionTime = ActionTime;
			IslookAround = false;
			HaveSeen = false;
		}
		if (Target.CompareTag ("ChumBucket")) {
			GetComponent<Animator> ().SetBool ("IsChumSearching", true);
			GetComponent<Animator> ().SetBool ("IsWalk", false);
		}
		if (Target.CompareTag ("Bench")) {
			GetComponent<Animator> ().SetBool ("IsBenchSleep", true);
			GetComponent<Animator> ().SetBool ("IsWalk", false);
		}
		if (Target.CompareTag ("LandPoint")) {
			GetComponent<Animator> ().SetBool ("IsLandSleep", true);
			GetComponent<Animator> ().SetBool ("IsWalk", false);
		}
		if (CurActionTime >= ActionTime) {
			GetComponent<Animator> ().SetBool ("IsActionEnd", true);
			GetComponent<Animator> ().SetBool ("IsLookAround", false);
			GetComponent<Animator> ().SetBool ("IsLandSleep", false);
			GetComponent<Animator> ().SetBool ("IsBenchSleep", false);
			GetComponent<Animator> ().SetBool ("IsLookAround", false);
			GetComponent<Animator> ().SetBool ("IsChumSearching", false);

		}
		if (CurActionTime > ActionTime+4f) {
			IsAction = false;
			Target.GetComponent<IsBeezy> ().beezy = false;
			if (IsKicked) {
				RunAway ();
			} else {
				RandomTargetPoints (TargetPoints);
			}
			LookAroundResult = 0;
			GetComponent<Animator> ().SetBool ("IsWalk", true);
			GetComponent<Animator> ().SetBool ("IsActionEnd", false);
			IslookAround = false;
			HaveSeen = false;
			CurActionTime=0;
		}
	}
	void Die(){
		_player.layer = 10;
		/*GetComponent<Animator> ().SetBool ("IsWalk", false);
		GetComponent<Animator> ().SetBool ("IsLookAround",false);
		GetComponent<Animator> ().SetBool ("IsChumSearching", false);
		GetComponent<Animator> ().SetBool ("IsBenchSleep", false);
		GetComponent<Animator> ().SetBool ("IsActionEnd", false);
		GetComponent<Animator> ().SetBool ("IsLandSleep", false);
		GetComponent<Animator> ().SetBool ("IsRun", false);
		GetComponent<Animator> ().Stop ();*/
		GetComponent<NavMeshAgent> ().enabled = false;
		GetComponent<HomelessAI> ().enabled = false;
		GetComponent<Animator> ().enabled = false;
		GetComponent<ObjectHealth> ().enabled = false;
		foreach(Rigidbody body in Elements){
			body.isKinematic = false;
		}
	}
}
