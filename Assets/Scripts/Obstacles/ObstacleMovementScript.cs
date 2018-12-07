using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovementScript : MonoBehaviour
{

    // Use this for initialization
    public float lifeTime;
    public float fallSpeed = 8.0f;
    public float spinSpeed = 250.0f;
    void Start()
    {
        this.lifeTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        if (this.gameObject.transform.position.y <= 1)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
