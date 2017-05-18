using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HotKeySlot : MonoBehaviour
{
	public HotKeyPanel myHotKeyPanel;
	public int myID;

	private bool mous;
	void Start(){
		myHotKeyPanel = transform.parent.GetComponent<HotKeyPanel>(); 
		myID = transform.GetSiblingIndex ();
		transform.GetChild (0).GetComponent<Text> ().text = (myID+1).ToString();
	}


	void Update()
	{
		if (mous && Input.GetKeyDown(KeyCode.Mouse1) )
		{
			myHotKeyPanel.ClearSlot(myID);
		}
	}
	public void _MouseEnter()
	{
		mous = true;
	}
	public void _MouseExit()
	{
		mous = false;
	}
		
	public void press ()
	{
		myHotKeyPanel.selectSlot(myID);
	}

}