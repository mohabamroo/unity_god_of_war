using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneratorScript : MonoBehaviour {

    public GameObject[] rocks;
	// Use this for initialization
	void Start () {
        InvokeRepeating("generateObstacle", 0.0f, 2.5f);
	}
	
	// Update is called once per frame
	void Update () {

    }

    void generateObstacle()
    {
        Instantiate(rocks[Random.Range(0, rocks.Length)], new Vector3(Random.Range(-100, 100), Random.Range(30, 50), Random.Range(-100, 100)), Quaternion.identity);
        Instantiate(rocks[Random.Range(0, rocks.Length)], new Vector3(Random.Range(-100, 100), Random.Range(30, 50), Random.Range(-100, 100)), Quaternion.identity);
        Instantiate(rocks[Random.Range(0, rocks.Length)], new Vector3(Random.Range(-100, 100), Random.Range(30, 50), Random.Range(-100, 100)), Quaternion.identity);

    }
}
