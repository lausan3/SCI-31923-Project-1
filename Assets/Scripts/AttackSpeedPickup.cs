using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedPickup : MonoBehaviour, IPickup
{
    [Header("Pickup")] 
    public PlayerController pc;
    public PlayerStats ps;
    public PickupStats pis;
    public float attackSpeedPickupValue = 0.25f; // how much the pickup affects the health
    
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        pis = GameObject.Find("Game Manager").GetComponent<PickupStats>();

        attackSpeedPickupValue = pis.attackSpeedPickupValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pickUp();
        }
    }

    public void pickUp()
    {
        ps.attackSpeed -= attackSpeedPickupValue;
        pc.UpdateStats();
        Debug.Log("Picked up Attack Speed pickup! New Attack Speed: " + ps.attackSpeed);
        
        Destroy(gameObject);
    }
}
