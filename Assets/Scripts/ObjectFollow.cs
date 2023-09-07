using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform follow;

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.transform.position;
    }
}
