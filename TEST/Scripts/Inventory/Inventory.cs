using UnityEngine;
using System.Collections;



public class Inventory: MonoBehaviour
{
	public Item[] items;



	// Use this for initialization
	void Start ()
	{
	}

	public Item FindByID(int SearchableID){
		foreach (Item curItem in items) {
			if (curItem.Id == SearchableID) {
				return curItem;
			}
		}
		return null;
	}


}