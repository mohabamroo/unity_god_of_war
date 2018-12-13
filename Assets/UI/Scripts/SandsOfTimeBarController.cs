using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandsOfTimeBarController : MonoBehaviour {

	private float totalSOT = 100;
	public static float currentSOT = 100;
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3((currentSOT / totalSOT), 1, 1);
	}

	public bool pressed = false;
	void Update() {
		if(pressed){
			decreaseSOTContinous();
		}
	}
	
	// Update is called once per frame
	public void increaseSOT()
	{
		if (currentSOT == 100)
			return;
		currentSOT += 10;
		transform.localScale = new Vector3((currentSOT / totalSOT), 1, 1);
	}

	public void decreaseSOT() {
		if (currentSOT >= 10 || pressed)
			pressed = !pressed;
	}

	public void decreaseSOTContinous()
	{
		if (currentSOT <= 0){
			pressed = false;
			return;
		}
		currentSOT -= Time.deltaTime*8f;
		transform.localScale = new Vector3 ((currentSOT / totalSOT), 1, 1);
	}
}
