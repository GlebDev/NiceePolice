using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Console : MonoBehaviour {

	public Text _consoleLine;
	public float AutoRemoveDelay;


	private int MaxLinesCount;
	private float curTimeout;
	private GameObject[] Lines;
	private int curLineNum=-1;
	private GameObject curLine;
	private delegate void Del();

	// Use this for initialization
	void Start () {
		MaxLinesCount = GameObject.Find ("GameSettings").GetComponent<GameSettings> ().MaxConsoleLineCount;
		Lines = new GameObject[MaxLinesCount];
	}
	
	// Update is called once per frame
	void Update () {
		if (curLineNum >= 0) {
			CommitWithDealy (RemoveFirstConsoleLine, AutoRemoveDelay);
		}
	}

	private void AddConsoleLine(string LineText){
		if (curLineNum >= (MaxLinesCount - 1)) {
			RemoveFirstConsoleLine ();
		} 
		curLineNum++;
		curLine =(GameObject)Instantiate(_consoleLine.gameObject,transform);
		curLine.transform.GetComponent<Text> ().text = LineText;
		Lines [curLineNum] = curLine;
	}
	private void RemoveFirstConsoleLine(){
		GameObject.Destroy( Lines[0]);//.chi.GetChild(0).gameObject,delay);
		for(int i=0;i<Lines.Length-1;i++){
			Lines [i] = Lines [i + 1];
		}
		curLineNum--;
		//Debug.Log("Console.LineRemoved");
	}
	private void CommitWithDealy(Del _delagate,float delay){
		if (curTimeout <= delay) {
			curTimeout += Time.deltaTime;
		} else {
			_delagate();
			curTimeout = 0;
		}
	}
}
