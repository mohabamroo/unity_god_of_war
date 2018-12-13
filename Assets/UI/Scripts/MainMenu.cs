using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu2 : MonoBehaviour {

	GameObject mainMenuPanel;
	GameObject optionsPanel;
	GameObject howToPlayPanel;
	GameObject creditsPanel;
	public AudioSource menuMusic;

	// Use this for initialization
	void Start () {
		mainMenuPanel = GameObject.Find("Canvas").transform.Find("MainMenu").gameObject;
		optionsPanel = GameObject.Find("Canvas").transform.Find("Options").gameObject;
		howToPlayPanel = GameObject.Find("Canvas").transform.Find("HowToPlay").gameObject;
		creditsPanel = GameObject.Find("Canvas").transform.Find("Credits").gameObject;

		mainMenuPanel.SetActive(true);
		optionsPanel.SetActive(false);
		howToPlayPanel.SetActive(false);
		creditsPanel.SetActive(false);

		PlayerPrefs.SetInt("Music", 1);
		PlayerPrefs.SetInt("SFX", 1);
		PlayerPrefs.SetInt("Speech", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startGame(){
		PlayerPrefs.SetInt("level", 1);
		SceneManager.LoadScene("Intro");
	}

	public void quitGame(){
		 Application.Quit();
	}

	public void showOptions(){
		mainMenuPanel.SetActive(false);
		optionsPanel.SetActive(true);
		howToPlayPanel.SetActive(false);
		creditsPanel.SetActive(false);
	}

	public void showHowToPlay(){
		mainMenuPanel.SetActive(false);
		optionsPanel.SetActive(false);
		howToPlayPanel.SetActive(true);
		creditsPanel.SetActive(false);
	}

	public void showCredits(){
		mainMenuPanel.SetActive(false);
		optionsPanel.SetActive(false);
		howToPlayPanel.SetActive(false);
		creditsPanel.SetActive(true);
	}

	public void backToOptions(){
		mainMenuPanel.SetActive(false);
		optionsPanel.SetActive(true);
		howToPlayPanel.SetActive(false);
		creditsPanel.SetActive(false);
	}

	public void backToMainMenu(){
		mainMenuPanel.SetActive(true);
		optionsPanel.SetActive(false);
		howToPlayPanel.SetActive(false);
		creditsPanel.SetActive(false);
	}

	public void mute(string audioType){
		Text text = GameObject.Find("Canvas").transform.Find("Options").gameObject.transform.Find(audioType).gameObject.transform.Find ("Variable").GetComponent<Text>();
		
		if(PlayerPrefs.GetInt(audioType) == 0){
			PlayerPrefs.SetInt(audioType, 1);
			text.text = "On";
			if (audioType == "Music"){
				menuMusic.Play();
			}
		}
		else{
			PlayerPrefs.SetInt(audioType, 0);
			text.text = "Off";
			if (audioType == "Music"){
				menuMusic.Pause();
			}
		}
	}


}
