using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

	public GameObject Weapon;
	public Collider ShotTrigger;
	public AnimationClip KickAnimation;
	public float CurKickTime,AnimationTime;

	// Use this for initialization
	void Start () {
		ShotTrigger.enabled=false;
		AnimationTime = KickAnimation.length;
	}
	
	// Update is called once per frame
	void Update () {
		if (CurKickTime <= AnimationTime) {
			CurKickTime += Time.deltaTime;
		}
		if (Input.GetMouseButtonDown(0) && CurKickTime >= AnimationTime) {
			Weapon.GetComponent<Animator>().SetTrigger ("kick");
			ShotTrigger.enabled=true;
			CurKickTime = 0;
		}
		else if(CurKickTime >= AnimationTime){
			Weapon.GetComponent<Animator>().SetTrigger("idle");
			ShotTrigger.enabled=false;
		}
	}
}
