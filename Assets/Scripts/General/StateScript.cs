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
        skillPts = 1;

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
        int totalXP = player.GetComponent<PlayerMovementScript>().totalXP;
        if (totalXP > 4000)
            skillPts = 5;
        else
        {
            if (totalXP > 2000)
                skillPts = 4;
            else
            {
                if (totalXP > 1000)
                    skillPts = 3;
                else
                {
                    if (totalXP > 500)
                        skillPts = 2;
                    else
                        skillPts = 1;
                }
            }
        }
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
        float ava = skillPts - (movementPts + attackPts + healthPts);
        if (ava > 0)
        {
            player.GetComponent<PlayerMovementScript>().UpgradeSpeed();
            movementPts += 1f;
            //skillPts -= 1;
            UpdateText();
        }
    }

    public void IncreaseAttackPts()
    {
        float ava = skillPts - (movementPts + attackPts + healthPts);
        if (ava > 0)
        {
            player.GetComponent<PlayerMovementScript>().UpgradeAttackPoints();
            attackPts += 1;
            //skillPts -= 1;
            UpdateText();
        }
    }

    public void IncreaseHealthPts()
    {
        float ava = skillPts - (movementPts + attackPts + healthPts);
        if (ava > 0)
        {
            player.GetComponent<PlayerMovementScript>().UpgradeHealthPoints();
            healthPts += 1;
            //skillPts -= 1;
            UpdateText();
        }
    }

    public void UpdateText()
    {
        float ava = skillPts - (movementPts + attackPts + healthPts);
        pointsText.text = "Available Skill Points: " + ava;
        speedText.text = "Increases movement speed by 10% \nCurrent factor is " + (1 + (0.1 * movementPts));
        damageText.text = "Increases damage by 10% \nCurrent factor is " + (1 + (0.1 * attackPts));
        healthText.text = "Increases health by 10% \nCurrent value is " + player.GetComponent<PlayerMovementScript>().health + "/100";
    }
}
