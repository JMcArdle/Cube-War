using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour {
	public GameObject musicPlayer;

	void Awake(){
		if (GameObject.FindGameObjectsWithTag ("MusicPlayer").Length >= 2) {
			foreach (GameObject m in GameObject.FindGameObjectsWithTag ("MusicPlayer")) {
				if (m.name != "MusicPlayer1") {
					DestroyObject (m);
				} else {
					musicPlayer = m;
				}
			}
		} else {
			musicPlayer.name = "MusicPlayer1";
			DontDestroyOnLoad (musicPlayer);
		}
	}


	// Use this for initialization
	void Start () {
		
	}

	public void ToggleMusic(){
		if (musicPlayer.activeInHierarchy) {
			musicPlayer.SetActive (false);
		} else {
			musicPlayer.SetActive (true);
		}
	}
	
	public void StartGame(){
		SceneManager.LoadScene ("Playable", LoadSceneMode.Single);
	}

	public void ViewRules(){
		SceneManager.LoadScene ("Instructions", LoadSceneMode.Single);
	}

	public void QuitGame(){
		Application.Quit();
	}
}
