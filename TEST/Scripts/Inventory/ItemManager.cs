using UnityEngine;
using System.Collections;



public class ItemManager : MonoBehaviour {
	
	public HotKeyPanel CurHKPanel;
	public Transform PlaceForItem, PlaceForThrowing,PlaceForWeapon;
	public bool HideItems{
		get{
			return _hideItemsStatus;
		}
		set{
			_hideItemsStatus = value;
			if (value) {
				DestroyAllItems ();
			} else {
				SwithchItem(curItemNum);
			}
		}
	}

	[SerializeField]private bool _hideItemsStatus;
	private int curItemNum=0,oldItemNum=-1;
	[SerializeField]private GameObject CurItem;


	// Use this for initialization
	void Start () {
		SwithchItem(curItemNum);
		HideItems = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!_hideItemsStatus){
			if (Input.anyKeyDown  && int.TryParse(Input.inputString,out curItemNum) && oldItemNum!=curItemNum ){//(int.TryParse(Input.inputString) ? (int.Parse(Input.inputString)!=curItemNum) : false)) {
				SwithchItem(curItemNum-1);
				oldItemNum = curItemNum;
			}
			if(Input.GetAxis("Mouse ScrollWheel")>0){
				if (curItemNum < CurHKPanel.HKItems.Length-1) {
					curItemNum++;
				} else {
					curItemNum = 0;
				}
				SwithchItem(curItemNum);
				oldItemNum = curItemNum;

			}
			if(Input.GetAxis("Mouse ScrollWheel")<0){
				if (curItemNum >0) {
					curItemNum--;
				} else {
					curItemNum = CurHKPanel.HKItems.Length-1;
				}
				SwithchItem(curItemNum);
				oldItemNum = curItemNum;

			}
		}
	}

	void SwithchItem(int i){
		DestroyAllItems();
		CurHKPanel.ActiveSlotNum = i;
		CurHKPanel.Redraw ();
		if (i < CurHKPanel.HKItems.Length && i >= 0 && CurHKPanel.HKItems [i]) {
			
		
			if (CurHKPanel.HKItems[i].GetComponent<RangeWeapon> ()) {
				CurItem = Instantiate (CurHKPanel.HKItems[i].gameObject, PlaceForThrowing) as GameObject;
				//PlaceForThrowing.GetComponent<Throwing> ().FakeWeapon = CurItem.transform;
				//PlaceForThrowing.GetComponent<Throwing> ().enabled = true;

			} else if (CurHKPanel.HKItems[i].GetComponent<MeleeWeapon> ()) {
				CurItem = Instantiate (CurHKPanel.HKItems[i].gameObject, PlaceForWeapon) as GameObject;
				//PlaceForWeapon.GetComponent<AttackScript> ().Weapon = CurItem;
				//PlaceForWeapon.GetComponent<AttackScript> ().ShotTrigger = CurItem.GetComponent<Collider>();
				//PlaceForWeapon.GetComponent<AttackScript> ().enabled = true;
			} else {
				CurItem = Instantiate(CurHKPanel.HKItems[i].gameObject,PlaceForItem) as GameObject;

			}
			CurItem.SetActive(true);
		}
	}
	public void DestroyAllItems(){
		for (int i=0; i < PlaceForItem.childCount; i++) {
			Destroy(PlaceForItem.GetChild(i).gameObject);
		}
		for (int i=0; i < PlaceForThrowing.childCount; i++) {
			Destroy(PlaceForThrowing.GetChild(i).gameObject);
		}
		for (int i=0; i < PlaceForWeapon.childCount; i++) {
			Destroy(PlaceForWeapon.GetChild(i).gameObject);
		}
	}
}
