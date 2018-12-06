using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScript : MonoBehaviour
{
    public int level = 0;
    // Use this for initialization
    void Start()
    {
        this.level++;
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        }
    }
}
