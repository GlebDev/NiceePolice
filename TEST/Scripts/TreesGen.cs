using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class TreesGen : MonoBehaviour {
	public int Count;
	public int RandomX;
	public int RandomY;
	public Transform pref;
	void Start () 
	{
		for(int i=0; i<Count; i++)
		{
			Instantiate(pref,transform.position+new Vector3(Random.Range(-RandomX,RandomX),0,Random.Range(-RandomY,RandomY)), transform.rotation);
		}

	}
}
