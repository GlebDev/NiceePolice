using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Interact : MonoBehaviour {

	public Transform slot;
	public float ItemMovementSpeed = 7f;
	public RPB ProgressBar;
	public float TimeToInteract=1;
	public float AIMSize=1,TakeDistance=3;
	[Range (0.1f, 1f)] public float DecelerationInspector;
	public float Deceleration{
		get{return _deceleration; }
		set{
			_deceleration = value < 0.1f ? 0.1f : value > 1f ?  1f : value;

		}
	}

	private float _deceleration;
	private bool TakeEnabled;
	private bool Take;
	private float curProgr;
	private RaycastHit hit;
	private Transform Item;
	private float curTimeToInteract;
	private bool IsHaveHangingBody;
	private Transform body;
	private float WalkSpeed,RunSpeed,JumpSpeed;
	private delegate void Del();
	private InterfaceManager  _interfaceManager;


	// Use this for initializbation
	void Start () {
		_interfaceManager= GameObject.Find ("Interface").transform.GetComponent<InterfaceManager> ();
		Take = false;
		TakeEnabled=true;
		Deceleration = DecelerationInspector;
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 Direction = transform.TransformDirection(Vector3.forward);
		if (Input.GetKey(KeyCode.E) ) {
			if (!Take && TakeEnabled && Physics.Raycast (transform.position,transform.TransformDirection(Vector3.forward), out hit, TakeDistance)  && hit.transform.GetComponent<Rigidbody> ()) {
				if (hit.transform.CompareTag ("Enemy") && !IsHaveHangingBody && hit.transform.root.GetComponent<HomelessAI> ().IsDead) {
					//LoadBody ();
					CommitFuncWithProgressBarDelay(LoadBody);
				} else {
					Take = true;
					Item = hit.transform;
				}
			} 
			if(Physics.Raycast (transform.position,transform.TransformDirection(Vector3.forward), out hit, TakeDistance)  && hit.transform.GetComponent<prisonerTruck>() && IsHaveHangingBody ){
				CommitFuncWithProgressBarDelay(LoadBodyToTruck);
			}

		} else {
			Item = null;
			Take = false;
			ClearProgress ();
		}
		if (Take && Item) {
			if (Item.GetComponent<Rigidbody> ()) {
				Item.GetComponent<Rigidbody> ().velocity = (slot.position - Item.position) * ItemMovementSpeed;
			} else {
				Take = false;
				Item = null;
			}
		}

	}

	public void ClearProgress(){
		curProgr = 0;
		curTimeToInteract = 0;
		ProgressBar.SetProgress (0);
		ProgressBar.gameObject.SetActive (false);
	}

	private void TakeBody (Transform body){
		body.SetParent (transform.root);
		body.GetComponent<Animator> ().enabled=true;
		body.GetComponent<Animator> ().applyRootMotion = false;
		body.GetComponent<Animator> ().Play("bangle");
		transform.root.GetComponent<ItemManager> ().HideItems = true;
		_interfaceManager.InterfaceSwitch_enabled = false;
		WalkSpeed = transform.root.GetComponent<FirstPersonController> ().m_WalkSpeed;
		RunSpeed = transform.root.GetComponent<FirstPersonController> ().m_RunSpeed;
		JumpSpeed = transform.root.GetComponent<FirstPersonController> ().m_JumpSpeed;
		transform.root.GetComponent<FirstPersonController> ().m_WalkSpeed *= _deceleration;
		transform.root.GetComponent<FirstPersonController> ().m_RunSpeed *= _deceleration;
		transform.root.GetComponent<FirstPersonController> ().m_JumpSpeed *= _deceleration;
	}

	void LoadBody (){
		body = hit.transform.root;
		TakeBody (body);
		IsHaveHangingBody = true;
		TakeEnabled = false;
	}
	void LoadBodyToTruck (){
		hit.transform.GetComponent<prisonerTruck> ().AddPrisioner();
		Destroy (body.gameObject);
		IsHaveHangingBody = false;
		TakeEnabled = true;
		transform.root.GetComponent<ItemManager> ().HideItems = false;
		transform.root.GetComponent<InterfaceManager> ().InterfaceSwitch_enabled = true;
		transform.root.GetComponent<FirstPersonController> ().m_WalkSpeed = WalkSpeed;
		transform.root.GetComponent<FirstPersonController> ().m_RunSpeed  = RunSpeed;
		transform.root.GetComponent<FirstPersonController> ().m_JumpSpeed = JumpSpeed;
		transform.root.GetComponent<CharacterStats> ().AddMoney (body.GetComponent<EnemyStats>().remuneration);
	}



	void CommitFuncWithProgressBarDelay(Del _delegate){
		if (curProgr <= 1) {
			ProgressBar.gameObject.SetActive (true);
			curTimeToInteract += Time.deltaTime;
			curProgr = curTimeToInteract / TimeToInteract;
			ProgressBar.SetProgress (curProgr);
		} else {
			ClearProgress ();
			_delegate();
		}
	}

}
