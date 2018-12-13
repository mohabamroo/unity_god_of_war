using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayUIScript : MonoBehaviour {

	public GameObject minimapPanel;
	public GameObject gameOverPanel;
	// private SoundManager sound;
	
	GameObject pausePanel;
	GameObject tutorialPanel;

	GameObject gatePanel;
	GameObject roadBlockPanel;
	GameObject questPanel;
	GameObject SOTBar;
	GameObject mapCollectiblesPanel;
	Text tutorialText;

	// Use this for initialization
	void Start () {
		pausePanel  = GameObject.Find("GameplayUI").transform.Find("PausePanel").gameObject;
		// gameOverPanel = GameObject.Find("GameplayUI").transform.Find("GameOverPanel").gameObject;
		// minimapPanel = GameObject.Find("MinimapCanvas").gameObject;
		gatePanel = GameObject.Find("GameplayUI").transform.Find("GatePanel").gameObject;
		roadBlockPanel = GameObject.Find("GameplayUI").transform.Find("RoadBlockPanel").gameObject;
		questPanel = GameObject.Find ("GameplayUI").transform.Find ("QuestPanel").gameObject;
		SOTBar = GameObject.Find ("GameplayUI").transform.Find ("SOTBar").gameObject;
		mapCollectiblesPanel = GameObject.Find ("GameplayUI").transform.Find ("MapCollectablesPanel").gameObject;
		tutorialPanel =  GameObject.Find("GameplayUI").transform.Find("TutorialPanel").gameObject;
		tutorialText = tutorialPanel.transform.Find ("Text").GetComponent<Text>();
		pausePanel.SetActive(false);
		// gameOverPanel.SetActive(false);
		tutorialPanel.SetActive(false);
		gatePanel.SetActive (false);
		roadBlockPanel.SetActive (false);
		mapCollectiblesPanel.SetActive(false);

//		sound = GetComponent<SoundManager>();
		// soun	d = GameObject.FindGameObjectWithTag ("Manager").GetComponent<SoundManager>();
		// showTutorialWithText("Press shift to run");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)){
			pauseGame();
		}
	}

	public void showMiniMapCollectiblePanel(bool show){
		mapCollectiblesPanel.SetActive(show);
	}

	public void pauseGame(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
			// sound.Pause ("Pause");
		}
		else{
			Time.timeScale = 1;
			// sound.Pause ("Resume");
		}
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
		pausePanel.SetActive(!pausePanel.activeSelf);
	}

	public void restartLevel(){
		Time.timeScale = 1;
		int level = PlayerPrefs.GetInt("level");
		SceneManager.LoadScene("Level " + level);
	}

	public void quit(){
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
	}

	public void gameOver(){
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
		gameOverPanel.SetActive(true);
		SOTBar.SetActive (false);
		questPanel.SetActive (false);
		mapCollectiblesPanel.SetActive (false);
		minimapPanel.SetActive(false);
    }

	public void showTutorialWithText(string tutText){
		if(tutText!=null){
			tutorialText.text = tutText;
			tutorialPanel.SetActive(true);
		}
		else{
			tutorialPanel.SetActive(false);
		}		
	}

	// public void hideLockIcon(){
	// 	GameObject lockIcon = GameObject.Find("LockIcon").gameObject;
	// 	lockIcon.SetActive(false);
	// }

}
