using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")] 
    public float startingHealth = 20f;
    public float speed = 4f;
    public float attackDamage = 20f;
    public float IFrameTime = 0.02f;

    public float maxDistDelta = 0.01f;
    public Color damagedFlashColor = Color.red;
    private Color originalColor;
    
    private 
    private float health;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        health = startingHealth;

        originalColor = gameObject.GetComponent<Material>().color;
    }

    private void Update()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, maxDistDelta) * speed * Time.deltaTime;
    }

    public IEnumerator Hurt(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this);
        }
        
        // flash color
        Material mat = gameObject.GetComponent<Material>();
        mat.color = damagedFlashColor;

        yield return new WaitForSeconds(IFrameTime);

        mat.color = originalColor;
    }
}
