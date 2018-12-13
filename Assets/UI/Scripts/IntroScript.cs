using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {
	Animator m_Animator;
	// Use this for initialization
	void Start () {
		m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("IntroAnim")){
			PlayerPrefs.SetInt("level", 1);
			SceneManager.LoadScene("Level 1");	
		}
	}
}
