using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class Tree : MonoBehaviour {
	public Transform IObject;
	private float RandScale;
	// Use this for initialization
	void Start () {
		RaycastHit Hit;
		Ray ray = new Ray(transform.position, -transform.up);
		if(Physics.Raycast(ray,out Hit,1000) && Hit.transform.CompareTag("Terrain")){
			RandScale = Random.Range (0.9f, 1.1f);
			Quaternion HitRotation= Quaternion.FromToRotation(transform.up,Hit.normal);
			Transform cloneObject = Instantiate(IObject, Hit.point, HitRotation) as Transform;
			cloneObject.parent = Hit.transform;
			cloneObject.localScale = new Vector3(RandScale,RandScale,RandScale);
			Vector3 ThisRotation = cloneObject.transform.localEulerAngles;
			cloneObject.transform.localEulerAngles=new Vector3(ThisRotation.x,Random.Range(0,360),ThisRotation.z); 

		}
	}
}