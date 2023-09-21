using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Stats")] 
    public float startingHealth = 20f;
    public float speed = 4f;
    public float attackDamage = 20f;
    public float IFrameTime = 0.1f;

    public float maxDistDelta = 0.01f;
    public Color damagedFlashColor = Color.red;
    private Color originalColor;

    private TextMeshProUGUI healthText;
    private float health;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        health = startingHealth;
        healthText = transform.Find("Canvas").Find("Health Text").GetComponent<TextMeshProUGUI>();

        healthText.text = startingHealth.ToString();

        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, maxDistDelta);
    }

    public IEnumerator Hurt(float damage)
    {
        health -= damage;

        healthText.text = health.ToString();
        
        Debug.Log(name + ": " + health);
        
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        
        // flash color
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = damagedFlashColor;

        yield return new WaitForSeconds(IFrameTime);

        mat.color = originalColor;
    }
}
