using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HotKeyPanel : MonoBehaviour {

	public Inventory CurrentInventory;
	public RectTransform Slot,mouseSlot,ActiveSlotPrefab;
	public Item[] HKItems;
	public int ActiveSlotNum{
		get{
			return _activeSlotNum;
		}
		set{
			if(value>=0 && value <= HKItems.Length-1){
				_activeSlotNum = value;
			}
		}
	}

	private Item TempItem;
	private MouseItem mouseI;
	private int TempId;
	[SerializeField]private int _activeSlotNum;


	// Use this for initialization
	void Start () {
		mouseI = mouseSlot.transform.GetComponent<MouseItem>();
		CreateInventorySlots ();
		Redraw ();

	}

	// Update is called once per frame
	void Update () {
		//mouseSlot.transform.position = Input.mousePosition;
	}



	public void selectSlot(int ID)
	{
		if (mouseI.MouseItemId >= 0) {
			HKItems [ID] = CurrentInventory.items [mouseI.MouseItemId];
			mouseI.MouseItemId = -1;
		} else if (mouseI.MouseHotKeyItemId >= 0) {
			SwapItems (ref HKItems [ID], ref HKItems [mouseI.MouseHotKeyItemId]);
			mouseI.MouseHotKeyItemId = -1;
		} else if (HKItems [ID]) {
			mouseI.MouseHotKeyItemId = ID;
		}
		Redraw();
	}

	public void ClearSlot(int ID)
	{
		HKItems [ID] = null;
		Redraw();
	}

	public void SwapItems(ref Item item1,ref Item item2){
		TempItem = item1;
		item1 = item2;
		item2 = TempItem;

	}

	public void Redraw ()
	{
		//ClearSlots();
		//CreateInventorySlots ();
		for (int i = 0; i < HKItems.Length; i++) {
			if (i == _activeSlotNum) {
				transform.GetChild (i).GetComponent<RectTransform>().localScale = ActiveSlotPrefab.GetComponent<RectTransform>().localScale;
				transform.GetChild (i).GetComponent<Image>().color = ActiveSlotPrefab.GetComponent<Image>().color;
				transform.GetChild (i).GetChild (0).GetComponent<Text>().color = ActiveSlotPrefab.GetChild (0).GetComponent<Text>().color;
			} else {
				transform.GetChild (i).GetComponent<RectTransform>().localScale = Slot.GetComponent<RectTransform>().localScale;
				transform.GetChild (i).GetComponent<Image>().color = Slot.GetComponent<Image>().color;
				transform.GetChild (i).GetChild (0).GetComponent<Text>().color = Slot.GetChild (0).GetComponent<Text>().color;
			}

			if(HKItems[i] && HKItems[i].stack>0){
				transform.GetChild(i).GetChild(1).GetComponent<RawImage>().enabled = true;
				transform.GetChild(i).GetChild(1).GetComponent<RawImage>().texture = HKItems[i].Image;
				transform.GetChild(i).GetChild (2).GetComponent<Text> ().text = HKItems [i].stack.ToString();
			}else{
				HKItems [i] = null;
				transform.GetChild (i).GetChild (1).GetComponent<RawImage> ().enabled = false;
				transform.GetChild (i).GetChild (2).GetComponent<Text> ().text = "";
			}
				/*transform.GetChild (i).rotation.Set(140f,0,0,0);
				transform.GetChild (i).GetComponent<Image> ().color = new Color(255,255,255,150);//#FFFFFF96
				transform.GetChild (i).GetChild (0).GetComponent<Text> ().color = new Color(255,255,255,150);//"#FFFFFF96"*/
		}
		if (mouseI.MouseHotKeyItemId>=0 && HKItems[mouseI.MouseHotKeyItemId]){
			mouseSlot.GetChild(0).GetComponent<RawImage> ().enabled = true;
			mouseSlot.GetChild(0).GetComponent<RawImage>().texture = HKItems[mouseI.MouseHotKeyItemId].Image;
			mouseSlot.GetChild(1).GetComponent<Text>().text = HKItems[mouseI.MouseHotKeyItemId].stack.ToString();
			//Debug.Log (mouseSlot.GetChild(1).GetComponent<Text>().text);
		}else{
			mouseSlot.GetChild(0).GetComponent<RawImage>().enabled = false;
			mouseSlot.GetChild(1).GetComponent<Text>().text = "";
		}
	}

	private void CreateInventorySlots(){
		for (int i = 0; i < HKItems.Length; i++) {
			Instantiate (Slot, transform);
			/*if (i == _activeSlotNum) {
				Instantiate (ActiveSlotPrefab, transform);
			} else {
				Instantiate (Slot, transform);
			}*/
		}
	}
	private void ClearSlots(){
		for (int i=0; i < transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}
	}



}