using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StateScript : MonoBehaviour
{
    public int level = 0;
    // Use this for initialization
    Scene currentScene;

    void Start()
    {
        this.currentScene = SceneManager.GetActiveScene();
        this.level++;
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
}
