using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class treeRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 ThisRotation = transform.localEulerAngles;
		transform.localEulerAngles=new Vector3(ThisRotation.x,Random.Range(0,360),ThisRotation.z); 
	}
	

}
