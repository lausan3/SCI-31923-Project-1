using UnityEngine;


public class RangePickup : MonoBehaviour, IPickup
{
    [Header("Pickup")] 
    public PlayerController pc;
    public PlayerStats ps;
    public PickupStats pis;
    public float rangePickupValue = 0.5f; // how much the pickup affects the health
    
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        pis = GameObject.Find("Game Manager").GetComponent<PickupStats>();

        rangePickupValue = pis.rangePickupValue;
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
        ps.attackRange += rangePickupValue;
        pc.UpdateStats();
        
        Debug.Log("Picked up Range pickup! New Range: " + ps.attackRange);
        
        Destroy(gameObject);
    }
}
