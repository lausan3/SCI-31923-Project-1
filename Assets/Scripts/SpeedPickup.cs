using UnityEngine;

public class SpeedPickup : MonoBehaviour, IPickup
{
    [Header("Pickup")] 
    public PlayerController pc;
    public PlayerStats ps;
    public PickupStats pis;
    public float speedPickupValue = 1f; // how much the pickup affects the health
    
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        pis = GameObject.Find("Game Manager").GetComponent<PickupStats>();

        speedPickupValue = pis.speedPickupValue;
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
        ps.moveSpeed += speedPickupValue;
        pc.UpdateStats();
        Debug.Log("Picked up Speed pickup! New Speed: " + ps.moveSpeed);
        
        Destroy(gameObject);
    }
}