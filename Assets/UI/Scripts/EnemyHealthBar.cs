using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {
    
    private float totalHealthBar = 30;
    private float currentHealthBar = 30;
    private float bossTotalHealth = 200;
    private float currenBosstHealth = 200;


    public void getHit()
    {
        currentHealthBar = currentHealthBar - 10;
        transform.localScale = new Vector3(0.23f, (currentHealthBar/totalHealthBar), 0.25f);

    }

    public void hideHB()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
    public void bossGetHit()
    {
        currenBosstHealth = currenBosstHealth - 50;
        Debug.Log("Boss health" + currenBosstHealth);
        transform.localScale = new Vector3(0.39f, (currenBosstHealth / bossTotalHealth), 0.25f);

    }

}
