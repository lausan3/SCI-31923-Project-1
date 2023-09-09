using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public static float dmg;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC") && other.gameObject.layer == 3)
        {
            IEnemy enemyC = other.transform.GetComponent<IEnemy>();
            StartCoroutine(enemyC.Hurt(dmg));
        }
    }
}