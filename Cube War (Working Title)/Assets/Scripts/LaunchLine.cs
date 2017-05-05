using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchLine : MonoBehaviour {
	public static LaunchLine launchLine;

	// Use this for initialization
	void Start () {
		launchLine = this;
		Enabled (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Enabled(bool enable){
		this.gameObject.GetComponent<LineRenderer> ().enabled = enable;
		this.gameObject.transform.GetChild (0).gameObject.SetActive (enable);
	}

	public void SetLinePosition(int index,Vector3 pos){
		this.gameObject.GetComponent<LineRenderer> ().SetPosition (index, pos);
	}

	public void SetPointPosition(Vector3 pos){
		this.gameObject.transform.GetChild (0).gameObject.transform.position = pos;
	}

	public Color endColor{
		get{ return this.gameObject.GetComponent<LineRenderer> ().endColor;}
		set{ 
			this.gameObject.GetComponent<LineRenderer> ().startColor = new Color(value.r,value.g,value.b,0.5f);
			this.gameObject.GetComponent<LineRenderer> ().endColor = new Color(value.r,value.g,value.b,0.8f);
			this.gameObject.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = value;
		}
	}
}
