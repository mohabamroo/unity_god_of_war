using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarController : MonoBehaviour
{

    private float totalHealthBar = 100;
    private float currentHealthBar = 100;
    public Image healthbarBackground;
    // Use this for initialization
    void Start()
    {

        Color newColor = Color.green;
        newColor.g = currentHealthBar / totalHealthBar;
        newColor.r = 1 - (currentHealthBar / totalHealthBar);
        newColor.b = 0.15f;
        GetComponent<Image>().color = newColor;
    }


    public void getHit(int amount)
    {
        currentHealthBar -= amount;
        this.updateBar();
    }
    public void recoverHealth(int amount)
    {
        currentHealthBar += amount;
        this.updateBar();
    }

    void updateBar()
    {
        transform.localScale = new Vector3((currentHealthBar / totalHealthBar), 1, 1);

        Color newColor = Color.green;
        newColor.g = currentHealthBar / totalHealthBar;
        newColor.r = 1 - (currentHealthBar / totalHealthBar);
        newColor.b = 0.15f;
        GetComponent<Image>().color = newColor;
    }
    public void die()
    {
        currentHealthBar = 10;
        getHit(10);
    }
}
