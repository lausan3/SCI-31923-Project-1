using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Pickup")] 
    public PlayerController pc;
    public PlayerStats ps;
    public float healValue = 15f; // how much the pickup affects the health
    public float damagePickupValue = 10f; // how much the pickup affects the health
    public float attackSpeedPickupValue = 0.25f; // how much the pickup affects the health
    public float rangePickupValue = 0.5f; // how much the pickup affects the health
    public float speedPickupValue = 1f; // how much the pickup affects the health
    
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        ps = pc.gameObject.GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched Pickup");
        if (other.CompareTag("Player"))
        {
            pickUp();
        }
    }

    private void pickUp()
    {
        // we have a switch statement for each type of pickup, can't think of another way to do it more efficiently atm
        switch (name)
        {
            case "HealthPickup":
                pc.health += healValue;
                Debug.Log("Picked up Health pickup! New Health: " + pc.health);
                
                break;
            case "DamagePickup":
                ps.attackDamage += damagePickupValue;
                Debug.Log("Picked up Damage pickup! New Damage: " + ps.attackDamage);
                
                break;
            case "AttackSpeedPickup":
                ps.attackSpeed -= attackSpeedPickupValue;
                Debug.Log("Picked up Attack Speed pickup! New Attack Speed: " + ps.attackSpeed);
                
                break;
            case "RangePickup":
                ps.attackRange += rangePickupValue;
                Debug.Log("Picked up Range pickup! New Range: " + ps.attackRange);
                
                break;
            case "SpeedPickup":
                ps.moveSpeed += speedPickupValue;
                Debug.Log("Picked up Speed pickup! New Speed: " + ps.moveSpeed);
                
                break;
        }
        
        pc.UpdateStats();
        
        Destroy(gameObject);
    }
}
