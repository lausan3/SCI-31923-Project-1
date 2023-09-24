using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform follow;
    private bool isCamera = false;
    public float lerpT = 0.02f;

    private void Start()
    {
        if (transform.CompareTag("MainCamera"))
        {
            isCamera = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCamera)
        {
            Vector2 lerpedVal = Vector2.Lerp(transform.position, follow.transform.position, lerpT);
            transform.position = new Vector3(lerpedVal.x, lerpedVal.y, -9f);
            return;
        }
        
        transform.position = follow.transform.position;
    }
}
