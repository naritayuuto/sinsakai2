using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rcontroller : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.001f,0,0);
        if(transform.position.x < -15f)
        {
            
            transform.position = new Vector3(16, 6, 7);
        }
    }
}
