using System.Collections;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Stats")] 
    public float startingHealth = 20f;
    public float attackDamage = 20f;
    public float IFrameTime = 0.1f;
    public int expOnDeath = 10;

    public float maxDistDelta = 0.01f;
    public Color damagedFlashColor = Color.red;
    private Color originalColor;

    private float health;

    #region Private References

    private GameManager gm;
    private Transform player;
    private TextMeshProUGUI healthText;
    private Rigidbody2D rb;

    #endregion

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        
        health = startingHealth;
        healthText = transform.Find("Canvas").Find("Health Text").GetComponent<TextMeshProUGUI>();
        healthText.text = startingHealth.ToString();

        originalColor = gameObject.GetComponent<Renderer>().material.color;

        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    private void Update()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, maxDistDelta);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(other.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage));
        }
    }

    public IEnumerator Hurt(float damage, float knockbackForce)
    {
        // taking damage
        health -= damage;
        healthText.text = health.ToString();
        
        if (health <= 0)
        {
            gm.enemiesKilled += 1;
            gm.exp += expOnDeath;
            Destroy(this.gameObject);
        }
        
        // knockback
        Vector2 knockbackDir = (transform.position - player.position).normalized;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        
        // flash color
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = damagedFlashColor;

        yield return new WaitForSeconds(IFrameTime);

        mat.color = originalColor;
    }
}
