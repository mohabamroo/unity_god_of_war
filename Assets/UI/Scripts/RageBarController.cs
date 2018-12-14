using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageBarController : MonoBehaviour
{

    private float totalRage = 100;
    public float currentRage = 0;
    bool recharge = true;

    float rageActivatedTime;
    bool rageActivated = false;
    float rageInterval;

    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector3((currentRage / totalRage), 1, 1);
    }

    void Update()
    {
        if (this.rageActivated)
        {
            this.rageActivatedTime += Time.deltaTime;
            this.currentRage -= (this.currentRage * (this.rageInterval - this.rageActivatedTime)/230);
            transform.localScale = new Vector3((currentRage / totalRage), 1, 1);
            if (this.rageActivatedTime > this.rageInterval)
            {
                this.rageActivated = false;
                this.currentRage = 0;
            }
        }
    }

    public void increaseRageMeter(int amount)
    {
        this.currentRage += amount;
        transform.localScale = new Vector3((currentRage / totalRage), 1, 1);
    }

    public void useRage(float interval)
    {
        this.rageActivatedTime = 0;
        this.rageActivated = true;
        this.rageInterval = interval;
    }

}
