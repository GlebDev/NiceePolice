using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public float SpeedAmplitude;
	public CloudGen GENERATOR;
	private float speed=1;
	//public Vector3 direction=new Vector3(0,-1,0);

	// Use this for initialization
	void Start () {
		GENERATOR = transform.root.GetComponent<CloudGen> (); 
		speed += Random.Range (-SpeedAmplitude, SpeedAmplitude);
		GetComponent<Animator> ().speed = speed;
		//Debug.Log (GENERATOR.GetComponent<CloudGen>().Z);
	}
	
	// Update is called once per frame
	/*void LateUpdate () {
		transform.Translate (direction * speed * Time.deltaTime);
		if(transform.position.z < -GENERATOR.GetComponent<CloudGen>().Z){
			Destroy (gameObject);
		}
	}*/
	void Destroy (){
		GENERATOR.CurCount--;
		Destroy (transform.parent.gameObject);
	}
}
