using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            this.loadNextLevel();
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
        player.GetComponent<PlayerMovementScript>().UpgradeSpeed();
        movementPts += 1f;
        skillPts -= 1;
    }

    public void IncreaseAttackPts()
    {
        player.GetComponent<PlayerMovementScript>().UpgradeAttackPoints();
        attackPts += 1;
        skillPts -= 1;
    }

    public void IncreaseHealthPts()
    {
        player.GetComponent<PlayerMovementScript>().UpgradeHealthPoints();
        healthPts += 1;
        skillPts -= 1;
    }
}
