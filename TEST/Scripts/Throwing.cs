using UnityEngine;
using System.Collections;

public class Throwing : MonoBehaviour {

	public Transform ThrowingWeaponPrefab;
	public int force=1300;
	public Inventory CurInv;
	public ItemManager CurItemManager;
	public Transform SpawnPoint;



	public Item FakeWeaponItem;
	private float CurThrowTime=0,AttackSpeed;
	private Transform CurWeapon;

	// Use this for initialization
	void Start () {
		CurInv = transform.root.GetComponent<Inventory> ();
		CurItemManager = transform.root.GetComponent<ItemManager> (); 
		SpawnPoint = transform.parent;
		AttackSpeed = transform.GetComponent<RangeWeapon> ().AttackSpeed <= 0 ? 1 : transform.GetComponent<RangeWeapon> ().AttackSpeed; 

		if (CurInv.FindByID (transform.GetComponent<RangeWeapon> ().Id)) {
			FakeWeaponItem = CurInv.FindByID (transform.GetComponent<RangeWeapon> ().Id);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (CurThrowTime < 60f/AttackSpeed) {
			CurThrowTime += Time.deltaTime;
		}else if (Input.GetMouseButtonDown(0) && FakeWeaponItem.stack>0) {
			transform.GetComponent<Animator>().SetTrigger("Throw");
			Throw();
			CurThrowTime = 0;
			if (CurInv.FindByID (transform.GetComponent<RangeWeapon> ().Id)) {
				FakeWeaponItem = CurInv.FindByID (transform.GetComponent<RangeWeapon> ().Id);
			}
			FakeWeaponItem.stack--;
			if (FakeWeaponItem.stack <= 0) {
				CurItemManager.DestroyAllItems ();
			}
			CurItemManager.CurHKPanel.Redraw ();

		}

	}

	void Throw(){
		CurWeapon = Instantiate (ThrowingWeaponPrefab, SpawnPoint.position, Quaternion.identity) as Transform;
		CurWeapon.GetComponent<PropellingWeapon> ().damage = transform.GetComponent<RangeWeapon> ().damage;
		CurWeapon.GetComponent<Rigidbody> ().AddForce(new Vector3(Camera.main.transform.forward.x,Camera.main.transform.forward.y+0.07f,Camera.main.transform.forward.z)*force);
		//CurRock.GetComponent<Rigidbody> ().AddRelativeTorque( new Vector3(500000000,500000000,5000000000));
	}
}
