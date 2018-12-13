using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarController : MonoBehaviour {

    private float totalHealthBar = 100;
    private float currentHealthBar = 100;
    public Image healthbarBackground;
	// Use this for initialization
	void Start () {
      
        Color newColor = Color.green;
        newColor.g = currentHealthBar/totalHealthBar ;
        newColor.r = 1 - (currentHealthBar/totalHealthBar);
        newColor.b = 0.15f;
        GetComponent<Image>().color = newColor;
	}
	

   public void getHit()
    {
        currentHealthBar -= 10;
        Debug.Log("prince health" + currentHealthBar);
        // if (currentHealthBar >0)
        transform.localScale = new Vector3((currentHealthBar / totalHealthBar), 1, 1);
          
        Color newColor = Color.green;
        newColor.g = currentHealthBar / totalHealthBar;
        newColor.r = 1 - (currentHealthBar / totalHealthBar);
        newColor.b = 0.15f;
        GetComponent<Image>().color = newColor;
    }

	public void getHealth()
	{
		if (currentHealthBar == 100)
			return;

		currentHealthBar += 20;
		getHit ();
	}

    public void die()
    {
        currentHealthBar = 10;
        getHit();
    }
}
