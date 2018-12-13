using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPieceController : MonoBehaviour {

	private GameObject topRight;
	private GameObject topLeft;
	private GameObject bottomRight;
	private GameObject bottomLeft;
	private int mapCounter;

	// Use this for initialization
	void Start () {
		topRight  = GameObject.Find("GameplayUI").transform.Find("MapCollectablesPanel").transform.Find("TopRight").gameObject;
		topLeft  = GameObject.Find("GameplayUI").transform.Find("MapCollectablesPanel").transform.Find("TopLeft").gameObject;
		bottomRight  = GameObject.Find("GameplayUI").transform.Find("MapCollectablesPanel").transform.Find("BottomRight").gameObject;
		bottomLeft  = GameObject.Find("GameplayUI").transform.Find("MapCollectablesPanel").transform.Find("BottomLeft").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void collectedMapPiece(){
		switch (mapCounter)
		{
			case 0: topRight.SetActive(true); break;
			case 1: bottomLeft.SetActive(true); break;
			case 2: bottomRight.SetActive(true); break;
			case 3: topLeft.SetActive(true); break;
		}
		mapCounter++; 
	}
}
