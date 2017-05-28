using UnityEngine;
using System.Collections;
using CustomDateTime;

public class TimeFlow : MonoBehaviour {

	public delegate void MethodContainer();
	public event MethodContainer onDay;
	public event MethodContainer onNight;
	public DateTime MyTime;

	[SerializeField] private int DurationOfTheDay;  //in minutes
	[SerializeField] private Animator Sun;

	private int StartTimeHours;
	private int StartDayTime,StartNightTime;
	private DateTime StartTime;
	private InterfaceManager IM;
	private float CurTime; 
	private bool IsDay;
	//private Quaternion OrigRot, RotationX;
	//private float TimeColorModifier;


	void Start () {
		StartTimeHours = GameObject.Find ("GameSettings").GetComponent<GameSettings> ().StartTimeHours;
		StartDayTime = GameObject.Find ("GameSettings").GetComponent<GameSettings> ().StartDayTime;
		StartNightTime = GameObject.Find ("GameSettings").GetComponent<GameSettings> ().StartNightTime;
		StartTime = new DateTime(StartTimeHours,0);
		MyTime = new DateTime(StartTimeHours,0);
		if (MyTime.Hours < 5 || MyTime.Hours > 21) {
			IsDay = false;
		} else {
			IsDay = true;
		}
		IM = GameObject.Find ("Interface").transform.GetComponent<InterfaceManager> ();
		if (DurationOfTheDay<0 || DurationOfTheDay>2400) {
			DurationOfTheDay = 24;
		}

		Sun.Play("SunRotate",0,MyTime.Hours/24f);
		Sun.speed = 24 / DurationOfTheDay;
	}

	void Update(){
		MyTime.SetTimeInMinutes((int)(StartTime.GetTimeInMinutes()+ Time.time * (24/DurationOfTheDay) ) );
		IM.SetTimePanel(MyTime.GetString());
		if(MyTime.Hours == StartDayTime && !IsDay){
			if(onDay != null ){
				onDay ();
				IsDay = true;
			}
		}
		if(MyTime.Hours == StartNightTime && IsDay){
			if(onNight != null){
				onNight ();
				IsDay = false;
			}
		}
		/*//RotationX = Quaternion.AngleAxis (Time.time * (24/DurationOfTheDay)*0.25f,Vector3.right);
		//Sun.transform.rotation = OrigRot * RotationX;
		TimeColorModifier = Mathf.Sin(Sun.transform.eulerAngles.x * Mathf.Deg2Rad);
		if(TimeColorModifier <0 ){
			TimeColorModifier = 0;
		}	
		//Debug.Log (Sun.transform.rotation.x + " + " + Sun.transform.eulerAngles.x * Mathf.Deg2Rad);
		//RenderSettings.ambientLight = new Color(AmbientColorModifier,AmbientColorModifier, AmbientColorModifier);
		//Sun.color = Color.Lerp(NightSunColor,OriginalSunColor,TimeColorModifier);
		RenderSettings.ambientLight=Color.Lerp(NightAmbienrColor,OriginalAmbientColor,TimeColorModifier);
		RenderSettings.fogColor=Color.Lerp(NightFogColor,OriginalFogColor,TimeColorModifier);
		if(Sun.transform.eulerAngles.x>190){
			Sun.color=NightSunColor;
		}else{
			Sun.color=OriginalSunColor;
		}*/
	}

}
