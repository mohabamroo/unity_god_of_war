using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
