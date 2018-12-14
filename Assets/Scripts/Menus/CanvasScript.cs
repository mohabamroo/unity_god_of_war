using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CanvasScript : MonoBehaviour {

    //Screens
    GameObject MainMenuScreen;
    GameObject OptionsScreen;
    GameObject GameScreen;
    GameObject PauseScreen;
    GameObject SkillUpgradeScreen;
    GameObject GameOverScreen;
    GameObject HowToPlayScreen;
    GameObject CreditsScreen;


    //Sliders
    //public Slider MusicSlider;
    //public Slider SpeechSlider;
    //public Slider EffectsSlider;

    public AudioMixer mixer;

    public GameObject player;

    public Animator creditsAnim;

    GameObject gameOverScreen;
    GameObject gameScreen;
    GameObject creditsScreen;
    public AudioSource backgroundMusic;
    public AudioClip GameMusic;

    private void Awake()
    {
        Time.timeScale = 0;
        player.GetComponent<FlyCameraScript>().enabled = false;

    }

    // Use this for initialization
    void Start ()
    {
        gameOverScreen = GameObject.Find("GameplayUI").transform.Find("GameOverPanel").gameObject;
        gameScreen = GameObject.Find("GameplayUI").transform.Find("GameScreen").gameObject;
        creditsScreen = GameObject.Find("GameplayUI").transform.Find("CreditsScreen").gameObject;
        print(creditsScreen);
        //MainMenuScreen = this.gameObject.transform.GetChild(0).gameObject;
        //OptionsScreen = this.gameObject.transform.GetChild(1).gameObject;
        //GameScreen = this.gameObject.transform.GetChild(2).gameObject;
        //PauseScreen = this.gameObject.transform.GetChild(3).gameObject;
        //SkillUpgradeScreen = this.gameObject.transform.GetChild(4).gameObject;
        //GameOverScreen = this.gameObject.transform.GetChild(5).gameObject;
        //HowToPlayScreen = this.gameObject.transform.GetChild(6).gameObject;
        //CreditsScreen = this.gameObject.transform.GetChild(7).gameObject;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void StartGame()
    {
        backgroundMusic.clip = GameMusic;
        backgroundMusic.Play();
        Time.timeScale = 1;
        player.GetComponent<FlyCameraScript>().enabled = true;
    }

    public void OpenOptions()
    {
        //MainMenuScreen.SetActive(false);
        //OptionsScreen.SetActive(true);
        //GameScreen.SetActive(false);
        //PauseScreen.SetActive(false);
        //SkillUpgradeScreen.SetActive(false);
        //GameOverScreen.SetActive(false);
        //HowToPlayScreen.SetActive(false);
        //CreditsScreen.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        //MainMenuScreen.SetActive(false);
        //OptionsScreen.SetActive(false);
        //GameScreen.SetActive(false);
        //PauseScreen.SetActive(false);
        //SkillUpgradeScreen.SetActive(false);
        //GameOverScreen.SetActive(false);
        //HowToPlayScreen.SetActive(true);
        //CreditsScreen.SetActive(false);
    }

    public void OpenCredits()
    {
        Time.timeScale = 0;
        gameScreen = GameObject.Find("GameplayUI").transform.Find("GameScreen").gameObject;
        creditsScreen = GameObject.Find("GameplayUI").transform.Find("CreditsScreen").gameObject;
        creditsScreen.SetActive(true);
        gameScreen.SetActive(false);
        creditsAnim = creditsScreen.transform.Find("Credits").GetComponent<Animator>();
        creditsAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void OpenUpgradeMenu()
    {
        //MainMenuScreen.SetActive(false);
        //OptionsScreen.SetActive(false);
        //GameScreen.SetActive(false);
        //PauseScreen.SetActive(false);
        //SkillUpgradeScreen.SetActive(true);
        //GameOverScreen.SetActive(false);
        //HowToPlayScreen.SetActive(false);
        //CreditsScreen.SetActive(false);
    }


    public void QuitToMainMenu()
    {
        Time.timeScale = 0;
        player.GetComponent<FlyCameraScript>().enabled = false;
        PlayerPrefs.SetInt("Paused", 1);
    }

    public void ResumeGame()
    {
        backgroundMusic.clip = GameMusic;
        backgroundMusic.Play();
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Paused", 0);
        //GameScreen.SetActive(true);
        //MainMenuScreen.SetActive(false);
        //OptionsScreen.SetActive(false);
        //PauseScreen.SetActive(false);
        //SkillUpgradeScreen.SetActive(false);
        //GameOverScreen.SetActive(false);
        //HowToPlayScreen.SetActive(false);
        //CreditsScreen.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMusicLevel(float musicVol)
    {
        mixer.SetFloat("musicVol", Mathf.Log10(musicVol)*20);
    }

    public void SetSpeechLevel(float speechVol)
    {
        mixer.SetFloat("speechVol", Mathf.Log10(speechVol) * 20);
    }

    public void SetEffectsLevel(float sfxVol)
    {
        mixer.SetFloat("sfxVol", Mathf.Log10(sfxVol) * 20);
    }

    public void GameOver()
    {
        player.GetComponent<FlyCameraScript>().enabled = false;
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        gameScreen.SetActive(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("MohabScene");
    }

    public void RestartLevel()
    {
        //TODO: Restart current Level
    }

    public void UpgradeMovement()
    {
        //TODO: Upgrade Player Movement
        //Increment Player Movement if applicable
    }

    public void UpgradeAttack()
    {
        //TODO: Upgrade Player Attack
        //Increment Player Attack if applicable
    }

    public void UpgradeHealth()
    {
        //TODO: Upgrade Player Level
        //Increment Player Health if applicable
    }
}
