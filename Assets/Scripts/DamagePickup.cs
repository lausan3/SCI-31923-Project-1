using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : MonoBehaviour, IPickup
{
    [Header("Pickup")] 
    public PlayerController pc;
    public PlayerStats ps;
    public PickupStats pis;
    public float damagePickupValue = 10f; // how much the pickup affects the health
    
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        pis = GameObject.Find("Game Manager").GetComponent<PickupStats>();

        damagePickupValue = pis.damagePickupValue;
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
        ps.attackDamage += damagePickupValue;
        pc.UpdateStats();
        Debug.Log("Picked up Damage pickup! New Damage: " + ps.attackDamage);
        
        Destroy(gameObject);
    }
}
