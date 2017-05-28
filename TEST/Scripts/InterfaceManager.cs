using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class InterfaceManager : MonoBehaviour {

	public InventoryPanel CharacterInventoryPanel;
	public HotKeyPanel CharacterHotKeyPanelPanel;
	//public MoneyPanel _moneyPanel;
	public Transform _AIM; 
	public bool InterfaceSwitch_enabled;
	public FirstPersonController CharacterFPC;
	public RPB EnergyProgressBar,BackGroundEnergyProgressBar;
	public float EnergyProgressBarDyingDelay;
	public TextPanel _timePanel,_moneyPanel;
	[SerializeField]private CharacterStats _characterStats;

	[SerializeField]private float _curEnergyPBTime;
	private bool CurVisible;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I) && InterfaceSwitch_enabled) {
			SetInterfaceVisible (!CurVisible);
			CharacterFPC.transform.GetComponent<ItemManager> ().HideItems = CurVisible;
		}


		//-------------------------- EnergyProgresBar ------------------------//
		if (_curEnergyPBTime <= EnergyProgressBarDyingDelay) {
			_curEnergyPBTime += Time.deltaTime;
		} else {
			EnergyProgressBar.SetTransparent(0);
			BackGroundEnergyProgressBar.SetTransparent(0);
		}
	}

	public void SetInterfaceVisible(bool flag){
		if(!flag){
			CharacterInventoryPanel.DescriptionPanel.gameObject.SetActive (flag);
		}
		CharacterInventoryPanel.gameObject.SetActive (flag);
		CharacterFPC.m_MouseLook.SetCursorLock(!flag);
		_timePanel.gameObject.SetActive (flag);
		_moneyPanel.gameObject.SetActive (flag);
		SetMoneyPanel("MONEY: " + _characterStats.SumOfMoney.ToString());
		_AIM.gameObject.SetActive (!flag);

		foreach (HotKeySlot slot in CharacterHotKeyPanelPanel.transform.GetComponentsInChildren<HotKeySlot>())
		{
			slot.enabled = flag;
		}
		CurVisible = flag;
	}

	public void SetEnergyProgress(float progress){
		EnergyProgressBar.SetTransparent(1);
		BackGroundEnergyProgressBar.SetTransparent(1);
		EnergyProgressBar.SetProgress (progress);
		_curEnergyPBTime = 0;
	}

	public void SetTimePanel(string timeStr){
		_timePanel.Redraw (timeStr);
	}

	public void SetMoneyPanel(string moneyStr){
		_moneyPanel.Redraw (moneyStr);
	}
}
