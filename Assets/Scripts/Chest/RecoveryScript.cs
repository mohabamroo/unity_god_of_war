using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryScript : MonoBehaviour
{

    public int chestHP;
    bool chestUsed;
    public AudioSource chestOpenSource;
    // Use this for initialization
    
    void Start()
    {
        chestOpenSource = transform.GetComponent<AudioSource>();
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
            if(!chestUsed) {
                chestOpenSource.Play();
                print("health increase");
                GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovementScript>().increaseHealthWithChest();
                this.chestUsed = true;
            }
        }
    }
}
