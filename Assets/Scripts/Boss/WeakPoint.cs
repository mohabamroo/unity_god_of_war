using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour {

    public int weakPoint;
    GameObject boss;
	// Use this for initialization
	void Start () {
        boss = GameObject.FindGameObjectsWithTag("Boss")[0];
    }
	
	// Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            boss.GetComponent<BossScript>().incrementHits(weakPoint);
        }
    }

   
}
