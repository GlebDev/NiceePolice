using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour{

	public int CloudCount;
	public int MaxHomelessCount;
	public int difficultyLevel;
	public int MaxHomelessCountInTruck;
	public int AmountPenalty;
	public List<Transform> TargetPoints = new List<Transform>();
	public List<Transform> EscapePoints = new List<Transform>();
	public float MeleeWeaponDamageMultipler;
	public float MeleeRangeDamageMultipler;
	public int MaxConsoleLineCount;

}
