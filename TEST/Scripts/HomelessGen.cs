using UnityEngine;
using System.Collections;

public class HomelessGen : MonoBehaviour {
	public GameSettings Settings;
	public int CurCount;
	public float timeout=20f,curTimeout;
	public Transform[] HomelessTypesArr;
	private Transform SpawnPoint;
	private int RndSpawnLoc,RndHomlessType;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("GameSettings").GetComponent<GameSettings> ()) {
			Settings = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (curTimeout <= timeout) {
			curTimeout += Time.deltaTime;
		}
		if(CurCount<Settings.MaxHomelessCount && curTimeout > timeout){
			curTimeout = 0;
			RndSpawnLoc = Random.Range (0,transform.childCount);
			SpawnPoint = transform.GetChild (RndSpawnLoc);
			GenerateRndHomless(SpawnPoint);
			CurCount++;
		}
	}

	void GenerateRndHomless(Transform SpawnPoint){
		do {
			RndHomlessType = Random.Range (0, HomelessTypesArr.Length);//рандомний тип ворога
		} while(HomelessTypesArr [RndHomlessType].GetComponent<EnemyStats> ().dificulty > Settings.difficultyLevel); //знаходимо складність рандомного типу ворога і якщо ця складність  більше загальної складності, то ми знови генеруємо рандомний тип ворога, допоки його складність  не стане не більшою за загальну складність  
		Instantiate (HomelessTypesArr [RndHomlessType], SpawnPoint.position, HomelessTypesArr [RndHomlessType].rotation);//створюємо вказаний тип ворога в указоному місці
		Debug.Log ("RndHomlessType:"+RndHomlessType);
	}
}
