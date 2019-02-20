using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void quit(){
		Time.timeScale = 1;
		PlayerPrefs.SetInt("level", 1);
		SceneManager.LoadScene("MainMenu");
	}
}
