using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour {

	public Text TimeText;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Redraw(string str){
		TimeText.text = str;
	}
}
