using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StateScript : MonoBehaviour
{
    public int level = 0;
    public int skillPts;

    public float movementPts;
    public float attackPts;
    public float healthPts;
    // Use this for initialization
    Scene currentScene;

    GameObject player;

    public Text pointsText;
    public Text speedText;
    public Text healthText;
    public Text damageText;

    void Start()
    {
        this.currentScene = SceneManager.GetActiveScene();
        this.level++;
        skillPts = 0;

        movementPts = 0;
        attackPts = 0;
        healthPts = 0;

        player = GameObject.FindWithTag("Player");

        DontDestroyOnLoad(transform.gameObject);
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            this.loadNextLevel();
        }
        UpdateText();
    }

    public void loadNextLevel()
    {
        level++;
        skillPts += 1;
        if (currentScene.name == "BossScene")
        {
            // TODO: load credits scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("BossScene");

        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("BossScene");

        }
    }

    public void IncreaseMovementPts()
    {
        if (skillPts > 0)
        {
            player.GetComponent<PlayerMovementScript>().UpgradeSpeed();
            movementPts += 1f;
            skillPts -= 1;
            UpdateText();
        }
    }

    public void IncreaseAttackPts()
    {
        if (skillPts > 0)
        {
            player.GetComponent<PlayerMovementScript>().UpgradeAttackPoints();
            attackPts += 1;
            skillPts -= 1;
            UpdateText();
        }
    }

    public void IncreaseHealthPts()
    {
        if (skillPts > 0)
        {
            player.GetComponent<PlayerMovementScript>().UpgradeHealthPoints();
            healthPts += 1;
            skillPts -= 1;
            UpdateText();
        }
    }

    public void UpdateText()
    {
        pointsText.text = "Available Skill Points: " + skillPts;
        speedText.text = "Increases movement speed by 10% \nCurrent factor is " + (1 + (0.1 * movementPts));
        damageText.text = "Increases damage by 10% \nCurrent factor is " + (1 + (0.1 * attackPts));
        healthText.text = "Increases health by 10% \nCurrent value is " + player.GetComponent<PlayerMovementScript>().health+"/100";
    }
}
