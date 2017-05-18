using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class RPB : MonoBehaviour {

	public float CurrentAmount{
		get{
			return _currentAmount;
		}
		set{
			if (value >= 0 && value <= 1) {
				_currentAmount = value;
			}else if(value<0) {
				_currentAmount = 0;
			}else if(value>1){
				_currentAmount = 1;
			}
		}
	}

	private float _currentAmount;


	public void SetProgress(float progress){
		CurrentAmount = progress;
		GetComponent<Image> ().fillAmount = _currentAmount;
	}

	public void SetTransparent(float alpha){
		if (alpha < 0) {
			alpha = 0f;
		}
		if (alpha > 1) {
			alpha = 1f;
		}
		GetComponent<Image> ().CrossFadeAlpha(alpha,0.2f,false);
	}
}
