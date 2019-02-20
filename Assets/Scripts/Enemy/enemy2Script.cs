using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2Script : MonoBehaviour {

    public Animator animator;
    public GameObject projectile;
    public GameObject handProjectile;

    public float maxDistance;


    // Use this for initialization
    void Start ()
    {
        maxDistance = 8;
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("attack", false);
            animator.SetBool("attack", true);
        }
    }

    void Throw()
    {
        GameObject temp = Instantiate(projectile, new Vector3(handProjectile.transform.position.x, handProjectile.transform.position.y, handProjectile.transform.position.z), handProjectile.transform.rotation);

        Physics.IgnoreCollision(temp.transform.GetComponent<Collider>(), transform.GetComponent<Collider>());

        Vector3 direction = transform.forward;
        direction.y = 0;

        temp.GetComponent<Rigidbody>().AddForce(direction * 500);

        handProjectile.SetActive(false);

        Destroy(temp, 10.0f);
        Invoke("ActivateProjectile", 2.0f);
    }

    void ActivateProjectile()
    {
        handProjectile.SetActive(true);
        animator.SetBool("attack", false);
    }
}
