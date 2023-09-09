using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform follow;
    private bool isCamera = false;

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
            transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, -9f);
            return;
        }
        
        transform.position = follow.transform.position;
    }
}
