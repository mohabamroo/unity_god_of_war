using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampUIKey : MonoBehaviour {
	public Camera cam; 
	public Image uiImage;
	// Use this for initialization
	void Start () {
		if (cam == null)
			cam = Camera.main; 
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 uiPos = cam.WorldToScreenPoint(this.transform.position);
	 	uiImage.transform.position = uiPos;
	}
}
