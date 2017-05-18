using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour {

	public Inventory CurrentInventory;
	public RectTransform Slot,mouseSlot,DescriptionPanel;


	private Item TempItem;
	private Item[] CurItems;
	private MouseItem mouseI;
	[SerializeField]private int TempId;

	// Use this for initialization
	void Start () {
		mouseI = mouseSlot.transform.GetComponent<MouseItem> ();
		CreateInventorySlots ();
		Redraw ();
	}
	
	// Update is called once per frame
	void Update () {
		mouseSlot.transform.position = Input.mousePosition;

	}



	public void selectSlot(int ID)
	{
		CurItems = CurrentInventory.items;
		mouseI.MouseHotKeyItemId = -1;

		if (mouseI.MouseItemId >= 0) {

			if (CurItems[mouseI.MouseItemId] && CurItems [ID] && CurItems [ID].Id == CurItems [mouseI.MouseItemId].Id) {
				if (CurItems [mouseI.MouseItemId].stack > CurItems [ID].MaxStack - CurItems [ID].stack) {
					CurItems [mouseI.MouseItemId].stack -= CurItems [ID].MaxStack - CurItems [ID].stack;
					CurItems [ID].stack = CurItems [ID].MaxStack;
				} else if(ID!=mouseI.MouseItemId){
					CurItems [ID].stack += CurItems [mouseI.MouseItemId].stack;
					CurItems [mouseI.MouseItemId].stack = 0;

				}

			} else {
				SwapItems (ref CurItems [ID], ref CurItems [mouseI.MouseItemId]);
			}
			mouseI.MouseItemId = -1;
		} else if(CurItems[ID]){
				mouseI.MouseItemId = ID ;
		}



		/*
		if (MouseItem.items[0].items[0] && CurItems[ID] && CurItems[ID].Id == MouseItem.items[0].items[0].Id) {
			if (MouseItem.items[0].items[0].stack > CurItems[ID].MaxStack - CurItems[ID].stack){
				MouseItem.items[0].items[0].stack -= CurItems[ID].MaxStack - CurItems[ID].stack;
				Debug.Log ("maxStack: "+CurItems[ID].MaxStack);
				CurItems[ID].stack = CurItems[ID].MaxStack;
			}else{
				CurItems[ID].stack += MouseItem.items[0].items[0].stack;

				MouseItem.items[0].items[0].stack = 0;
			}
			if (MouseItem.items[0].items[0].stack == 0) {
				MouseItem.items[0].items[0] = null;
			}

		} else {
			SwapItems(ref CurItems[ID],ref MouseItem.items[0].items[0]);
			CurrentInventory.items = CurItems; 
		}*/
		//mouseI.MouseItemId = oldId;
		CurrentInventory.items = CurItems; 
		Redraw();
	}

	public void SetDescriptionPanel(int ID){
		if (CurItems [ID]) {
			DescriptionPanel.gameObject.SetActive (true);
			DescriptionPanel.GetChild (0).GetComponent<Text> ().text = CurItems [ID].Name;
			DescriptionPanel.GetChild (1).GetComponent<Text> ().text ="price:  "+  CurItems [ID].price+"\n" 
				+ (CurItems [ID].GetComponent<Weapon>() ? ("damage:  "+  CurItems [ID].GetComponent<Weapon>().damage+"\n") : "") 
				+  (CurItems [ID].GetComponent<Weapon>() ? ("attack speed:  "+  CurItems [ID].GetComponent<Weapon>().AttackSpeed+"\n") :"") 
				+CurItems [ID].description;
			DescriptionPanel.transform.position = new Vector3 (Input.mousePosition.x + DescriptionPanel.rect.width / 2, Input.mousePosition.y - DescriptionPanel.rect.height / 2, Input.mousePosition.z); 
		}
	}

	public void ClearDescriptionPanel(){
		DescriptionPanel.gameObject.SetActive(false);

	}

	public void SwapItems(ref Item item1,ref Item item2){
		TempItem = item1;
		item1 = item2;
		item2 = TempItem;

	}

	public void Redraw ()
	{
		CurItems = CurrentInventory.items;
		for (int i = 0; i < CurItems.Length; i++) {
			if(CurItems[i] && CurItems[i].stack>0 ){
				transform.GetChild(i).GetChild(0).GetComponent<RawImage>().enabled = true;
				transform.GetChild(i).GetChild(0).GetComponent<RawImage>().texture = CurItems[i].Image;
				transform.GetChild (i).GetChild (1).GetComponent<Text> ().text = CurItems [i].stack.ToString();
			}else{
				CurItems [i] = null;
				transform.GetChild (i).GetChild (0).GetComponent<RawImage> ().enabled = false;
				transform.GetChild (i).GetChild (1).GetComponent<Text> ().text = "";
			}
		}
		if (mouseI.MouseItemId>=0 && CurItems[mouseI.MouseItemId]){
			mouseSlot.GetChild(0).GetComponent<RawImage> ().enabled = true;
			mouseSlot.GetChild(0).GetComponent<RawImage>().texture = CurItems[mouseI.MouseItemId].Image;
			mouseSlot.GetChild(1).GetComponent<Text>().text = CurItems[mouseI.MouseItemId].stack.ToString();
		}else{
			mouseSlot.GetChild(0).GetComponent<RawImage>().enabled = false;
			mouseSlot.GetChild(1).GetComponent<Text>().text = "";
		}
	}

	private void CreateInventorySlots(){
		CurItems = CurrentInventory.items;
		for (int i = 0; i < CurItems.Length; i++) {
			Instantiate (Slot,transform);
		}
	}


}
