using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Slider MusicSlider;
    public Slider SpeechSlider;
    public Slider EffectsSlider;

    // Use this for initialization
    void Start () {
        MainMenuScreen = this.gameObject.transform.GetChild(0).gameObject;
        OptionsScreen = this.gameObject.transform.GetChild(1).gameObject;
        GameScreen = this.gameObject.transform.GetChild(2).gameObject;
        PauseScreen = this.gameObject.transform.GetChild(3).gameObject;
        SkillUpgradeScreen = this.gameObject.transform.GetChild(4).gameObject;
        GameOverScreen = this.gameObject.transform.GetChild(5).gameObject;
        HowToPlayScreen = this.gameObject.transform.GetChild(6).gameObject;
        CreditsScreen = this.gameObject.transform.GetChild(7).gameObject;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void StartGame(){
        MainMenuScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        GameScreen.SetActive(true);
        PauseScreen.SetActive(false);
        SkillUpgradeScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        HowToPlayScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }

    public void OpenOptions()
    {
        MainMenuScreen.SetActive(false);
        OptionsScreen.SetActive(true);
        GameScreen.SetActive(false);
        PauseScreen.SetActive(false);
        SkillUpgradeScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        HowToPlayScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        MainMenuScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        GameScreen.SetActive(false);
        PauseScreen.SetActive(false);
        SkillUpgradeScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        HowToPlayScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }

    public void OpenCredits()
    {
        MainMenuScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        GameScreen.SetActive(false);
        PauseScreen.SetActive(false);
        SkillUpgradeScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        HowToPlayScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void OpenUpgradeMenu()
    {
        MainMenuScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        GameScreen.SetActive(false);
        PauseScreen.SetActive(false);
        SkillUpgradeScreen.SetActive(true);
        GameOverScreen.SetActive(false);
        HowToPlayScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }


    public void QuitToMainMenu()
    {
        MainMenuScreen.SetActive(true);
        OptionsScreen.SetActive(false);
        GameScreen.SetActive(false);
        PauseScreen.SetActive(false);
        SkillUpgradeScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        HowToPlayScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Paused", 0);
        GameScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        PauseScreen.SetActive(false);
        SkillUpgradeScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        HowToPlayScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMusicLevel()
    {
        //TODO: Adjust Audio Level
        //MusicLevel = MusicSlider.value;
    }

    public void SetSpeechLevel()
    {
        //TODO: Adjust Speech Level
        //SpeechLevel = SpeechSlider.value;

    }

    public void SetEffectsLevel()
    {
        //TODO: Adjust Effects Level
        //EffectsLevel = EffectsSlider.value;
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
