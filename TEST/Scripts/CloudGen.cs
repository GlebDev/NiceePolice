using UnityEngine;
using System.Collections;


public class CloudGen : MonoBehaviour {
    
	private int MaxCount;
	public int Z=600;
	public int RandomY=50,RandomX=600;
	public float timeout=5f;
	public Transform[] clouds;

	[HideInInspector]public int CurCount;

	private Transform _curCloud;
	private float curTimeout;


	// Use this for initialization
	void Start () {
		if (GameObject.Find ("GameSettings").GetComponent<GameSettings> ()) {
			MaxCount = GameObject.Find ("GameSettings").GetComponent<GameSettings> ().CloudCount;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (curTimeout <= timeout) {
			curTimeout += Time.deltaTime;
		}
		if(clouds.Length>0 && CurCount<MaxCount && curTimeout > timeout){
			curTimeout = 0;
			_curCloud = clouds [Random.Range (0, clouds.Length)];	
			Instantiate(_curCloud,transform.position+new Vector3(Random.Range(-RandomX,RandomX),Random.Range(-RandomY,RandomY),Z),_curCloud.rotation,transform);
			CurCount++;
		}

	}
}
