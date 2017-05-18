using UnityEngine;
using System.Collections;

public class SlotButton : MonoBehaviour
{
	public InventoryPanel myInvPanel;
	public int myID;

	void Start(){
		myInvPanel = transform.parent.GetComponent<InventoryPanel>(); 
		myID = transform.GetSiblingIndex ();
	}

	public void press ()
	{
		myInvPanel.selectSlot(myID);
		myInvPanel.ClearDescriptionPanel();
	}
	public void OnPointEnter ()
	{
		myInvPanel.SetDescriptionPanel(myID);
	}
	public void OnMouseExit ()
	{
		myInvPanel.ClearDescriptionPanel();
	}
}