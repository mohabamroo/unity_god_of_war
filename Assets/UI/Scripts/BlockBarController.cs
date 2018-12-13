using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBarController : MonoBehaviour {

	private float totalBlock = 100;
	public float currentBlock = 100;
	bool recharge = true;
	private Animator player;
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3((currentBlock / totalBlock), 1, 1);
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
	}

	void Update () {
		if(!player.GetBool("block"))
			Recharge();
		if(currentBlock < 20)
			player.SetBool("canBlock", false);
		else player.SetBool("canBlock", true);
	}

	public void Recharge(){
		if (currentBlock < 100 && recharge == true){
			currentBlock += 5*Time.deltaTime;
			transform.localScale = new Vector3((currentBlock / totalBlock), 1, 1);
		}
	}
	
	// Update is called once per frame

	public IEnumerator decreaseBlock()
	{
		// print(currentBlock);
		if (currentBlock > 0){
			// currentBlock = 0;
			// The current block will stay negative for a short time so that he cant block immediately after the bar reaches 0
			currentBlock -= 20;
			transform.localScale = new Vector3 ((currentBlock / totalBlock), 1, 1);
		} else{
			recharge = false;
			yield return new WaitForSeconds (3);
			recharge = true;
		}
		// transform.localScale = new Vector3 ((currentBlock / totalBlock), 1, 1);
	}

}
