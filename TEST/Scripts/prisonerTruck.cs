using UnityEngine;
using System.Collections;

public class prisonerTruck : MonoBehaviour {

	public int MaxHomelessCount,CurHomlessCount=0;
	public TextMesh text;
	private HomelessGen _homelessGen;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("GameSettings").GetComponent<GameSettings> ()) {
			MaxHomelessCount = GameObject.Find ("GameSettings").GetComponent<GameSettings> ().MaxHomelessCountInTruck;
			text.text =  CurHomlessCount + "/" + MaxHomelessCount;
		}
		if (GameObject.Find ("HomelessGenerator").GetComponent<HomelessGen> ()) {
			_homelessGen = GameObject.Find ("HomelessGenerator").GetComponent<HomelessGen> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/*void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Enemy" )&& CurHomlessCount<MaxHomelessCount) {
			CurHomlessCount++;
			text.text =  CurHomlessCount + "/" + MaxHomelessCount;
			Destroy (other.transform.root.gameObject);
			if (GameObject.Find ("HomelessGenerator").GetComponent<HomelessGen> ()) {
				GameObject.Find ("HomelessGenerator").GetComponent<HomelessGen> ().CurCount--;
			}
			if (CurHomlessCount == MaxHomelessCount) {
				Leave ();
			}

		} 
	}*/

	public void AddPrisioner(){
		CurHomlessCount++;
		text.text =  CurHomlessCount + "/" + MaxHomelessCount;
		_homelessGen.CurCount--;
		if (CurHomlessCount == MaxHomelessCount) {
			Leave ();
		}

	}
	void Leave(){
		GetComponent<Animator> ().SetBool ("Leave", true);
		CurHomlessCount=0;
		text.text =  CurHomlessCount + "/" + MaxHomelessCount;
	}


}
