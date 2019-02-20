using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayUIScript : MonoBehaviour {

	public GameObject minimapPanel;
	public GameObject gameOverPanel;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioSource musicSource;
    public GameObject stateHolder;

    // private SoundManager sound;

    GameObject pausePanel;
    GameObject gamePanel;
    GameObject tutorialPanel;
    GameObject mainMenuPanel;

	GameObject gatePanel;
	GameObject roadBlockPanel;
    GameObject questPanel;
    GameObject searchPanel;
    GameObject SOTBar;
	GameObject mapCollectiblesPanel;
	Text tutorialText;

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
		pausePanel  = GameObject.Find("GameplayUI").transform.Find("PausePanel").gameObject;
        mainMenuPanel = GameObject.Find("GameplayUI").transform.Find("MainMenuScreen").gameObject;
        gameOverPanel = GameObject.Find("GameplayUI").transform.Find("GameOverPanel").gameObject;
		gamePanel = GameObject.Find("GameplayUI").transform.Find("GameScreen").gameObject;
        gatePanel = GameObject.Find("GameplayUI").transform.Find("GatePanel").gameObject;
        searchPanel = GameObject.Find("GameplayUI").transform.Find("SearchPanel").gameObject;
        roadBlockPanel = GameObject.Find("GameplayUI").transform.Find("RoadBlockPanel").gameObject;
		questPanel = GameObject.Find ("GameplayUI").transform.Find ("QuestPanel").gameObject;
		//SOTBar = GameObject.Find ("GameplayUI").transform.Find ("SOTBar").gameObject;
		//mapCollectiblesPanel = GameObject.Find ("GameplayUI").transform.Find ("MapCollectablesPanel").gameObject;
		tutorialPanel =  GameObject.Find("GameplayUI").transform.Find("TutorialPanel").gameObject;
		tutorialText = tutorialPanel.transform.Find ("Text").GetComponent<Text>();

		pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
		tutorialPanel.SetActive(false);
        gatePanel.SetActive(false);
        searchPanel.SetActive(false);
        questPanel.SetActive(false);
        roadBlockPanel.SetActive (false);
        //gamePanel.SetActive(false);

        DontDestroyOnLoad(musicSource);
    }

    // Update is called once per frame
    void FixedUpdate() { 
        if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape))
        {
			pauseGame();
		}
	}

	public void showMiniMapCollectiblePanel(bool show){
		mapCollectiblesPanel.SetActive(show);
	}

	public void pauseGame()
    {
        player.GetComponent<FlyCameraScript>().enabled = false;
    
        Time.timeScale = 0;
        musicSource.clip = menuMusic;
        musicSource.Play();
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
       
    }

    public void resumeGame() {
        player.GetComponent<FlyCameraScript>().enabled = true;
        Time.timeScale = 1;
        musicSource.clip = gameMusic;
        musicSource.Play();
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
     
    }
	public void restartLevel(){
		Time.timeScale = 1;
        //int level = PlayerPrefs.GetInt("level");
        //SceneManager.LoadScene("Level " + level);
        Destroy(player);
        Destroy(transform.gameObject);
        Destroy(stateHolder);
        if (SceneManager.GetActiveScene().name == "MohabScene")
        {
            SceneManager.LoadScene("MohabScene");
            mainMenuPanel.SetActive(false);

        }
        if (SceneManager.GetActiveScene().name == "BossScene")
        {
            SceneManager.LoadScene("BossScene");
        }
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(true);
        //gameOverScreen.SetActive(false);
        //pauseScreen.SetActive(false);
        //gameScreen.SetActive(true);
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
