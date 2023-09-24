using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IPickup
{
    [Header("Pickup")] 
    public PlayerController pc;
    public PickupStats pis;
    public float healValue = 15f; // how much the pickup affects the health
    
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pis = GameObject.Find("Game Manager").GetComponent<PickupStats>();

        healValue = pis.healValue;
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
        pc.health += healValue;
        Debug.Log("Picked up Health pickup! New Health: " + pc.health);
        
        Destroy(gameObject);
    }
}
