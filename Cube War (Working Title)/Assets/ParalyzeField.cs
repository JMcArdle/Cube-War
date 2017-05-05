using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalyzeField : MonoBehaviour {
	public UnitClass unit; 
	/*
	// Use this for initialization
	void Start () {
		unit = this.gameObject.GetComponentInParent<UnitClass> ();
		Physics.IgnoreCollision(this.gameObject.GetComponentInParent<Collider>(), 
			this.gameObject.GetComponent<Collider>());
	}

	void OnTriggerEnter (Collision other)
	{
		if (other.gameObject.GetComponent<Cube> () != null) {
			if (other.gameObject.GetComponent<UnitClass> ().owningPlayer != unit.owningPlayer
			     && other.gameObject.GetComponent<UnitClass> ().unitClass != className.Paralyze) {
				other.gameObject.GetComponent<Cube> ().stunned = true;
			}
		}
	}*/
}
