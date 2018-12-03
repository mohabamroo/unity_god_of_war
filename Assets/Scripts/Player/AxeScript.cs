using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour
{

    public Transform attachPoint;
    public Transform obj;

    void Start()
    {
        // obj.parent = attachPoint;
        // obj.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        print(attachPoint.position);
        // transform.position = attachPoint.position;
    }
}
