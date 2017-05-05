using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (StateMachine.turnState == Turn.pause) {
				StateMachine.unPause ();
			} else {
				StateMachine.pause ();
			}
		}
	}
}
