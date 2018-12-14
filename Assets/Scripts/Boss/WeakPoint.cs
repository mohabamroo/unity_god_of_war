using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour {

    public int weakPoint;
    GameObject boss;
    float lastHitTime;
	// Use this for initialization
	void Start () {
        boss = GameObject.FindGameObjectWithTag("Boss");
    }
	
	// Update is called once per frame
    void Update () {
		this.lastHitTime += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            if(this.lastHitTime < 2) {
                return;
            }
            this.lastHitTime = 0;
            boss.GetComponent<BossScript>().incrementHits(weakPoint);
        }
    }
}
