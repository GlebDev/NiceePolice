using UnityEngine;
using System.Collections;

public class SunManager : MonoBehaviour {
	
	[SerializeField] private Color NightAmbienrColor;
	[SerializeField] private Color NightFogColor;
	[SerializeField] private Color NightSunColor;

	private Color OriginalAmbientColor;
	private Color OriginalFogColor;
	private Color OriginalSunColor;
	private float TimeColorModifier;

	// Use this for initialization
	void Start () {
		OriginalAmbientColor = RenderSettings.ambientLight;
		OriginalSunColor = GetComponent<Light>().color; 
		OriginalFogColor = RenderSettings.fogColor;
	}

	void Update(){
		TimeColorModifier = Mathf.Sin(transform.eulerAngles.x * Mathf.Deg2Rad);
		if(TimeColorModifier <0 ){
			TimeColorModifier = 0;
		}	
		RenderSettings.ambientLight=Color.Lerp(NightAmbienrColor,OriginalAmbientColor,TimeColorModifier);
		RenderSettings.fogColor=Color.Lerp(NightFogColor,OriginalFogColor,TimeColorModifier);
	}
	public void SetNight () {
		GetComponent<Light>().color = NightSunColor;
	}

	public void SetDay () {
		GetComponent<Light>().color = OriginalSunColor;
	}
}
