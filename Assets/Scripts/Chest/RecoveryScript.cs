using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryScript : MonoBehaviour
{

    public int chestHP;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay(Collision col)
    {
        // Player in chest proximity and pressed E
        if (col.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            // call a function in the player class that maximizes/increases his HP
        }
    }
}
