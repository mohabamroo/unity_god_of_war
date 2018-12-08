using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    public ParticleSystem explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(transform.gameObject);
        ParticleSystem exp = Instantiate(explosion, collision.contacts[0].point, collision.gameObject.transform.rotation);
        Destroy(exp.gameObject, 1.0f);

        if(collision.gameObject.tag == "Player")
        {

        }
    }
}
