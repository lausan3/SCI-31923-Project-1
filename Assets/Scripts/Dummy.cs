using System.Collections;
using UnityEngine;
using TMPro;

class Dummy : MonoBehaviour, IEnemy
{
    [Header("Stats")] 
    public float startingHealth = 20f;
    public float attackDamage = 20f;
    public float IFrameTime = 0.02f;
    
    public Color damagedFlashColor = Color.red;

    private TextMeshProUGUI healthText;

    private Color originalColor;
    
    private float health;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        health = startingHealth;

        healthText = transform.Find("Canvas").Find("Health Text").GetComponent<TextMeshProUGUI>();

        healthText.text = health.ToString();
        
        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }

    public IEnumerator Hurt(float damage)
    {
        health -= damage;

        healthText.text = health.ToString();
        
        Debug.Log(name + ": " + health);

        if (health <= 0)
        {
            Destroy(this);
        }
        
        // flash color
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = damagedFlashColor;

        yield return new WaitForSeconds(IFrameTime);

        mat.color = originalColor;
    }
}