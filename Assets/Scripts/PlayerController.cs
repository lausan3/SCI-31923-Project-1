using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;


public class PlayerController : MonoBehaviour
{
    [RequiredMember]
    public PlayerStats stats;
    
    [Header("Speed")] 
    private float speed;

    [Header("Combat")] 
    public LayerMask enemyMask;
    public float IFrameTime = 0.05f;
    
    public GameObject attackType;
    public Transform aimIndicator;

    private Vector2 aimingDirection;
    private bool attacking;
    private float timeToAttackSecs;
    private Vector2 mousePos;

    #region Stats

    [HideInInspector] public float health;
    private float damage;
    // this should be a total range from transform.pos -> the end of the overlap circle
    private float range;
    private float attackSpeed;
    private float knockbackForce;

    #endregion

    private float timer;
    private Color originalColor;

    #region Private references

    private CircleCollider2D coli;
    private Rigidbody2D rb;
    private Material mat;

    #endregion

    private void Start()
    {
        UpdateStats();
        attacking = false;
        timer = attackSpeed;
        health = stats.maxHealth;
        coli = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        mat = GetComponent<Renderer>().material;
        originalColor = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        timer -= Time.deltaTime;

        if (timer <= 0.005 && !attacking)
        {
            StartCoroutine(Attack(0.1f));
            timer = attackSpeed;
        }
    }

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // rb.AddForce(Vector2.up * speed * Time.deltaTime, ForceMode2D.Impulse);
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // rb.AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Impulse);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            // rb.AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Impulse);
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // rb.AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Impulse);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        // mouse calculations
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimingDirection = (mousePos - (Vector2)transform.position).normalized;
        
        // aiming indicator position
        aimIndicator.position = new Vector3(transform.position.x + (aimingDirection.x * 1f),transform.position.y + (aimingDirection.y * 1f), 1f);
    }

    private IEnumerator Attack(float attackSpriteShowTime)
    {
        attacking = true;

        // Physics2D.OverlapCircleAll()
        float radius = (range - coli.radius) / 2;
        // this is the range to use for OverlapCircleAll
        Vector2 sendRange = (Vector2)transform.position + (aimingDirection * range) - (aimingDirection * new Vector2(radius, radius));
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(sendRange, radius, enemyMask);
        
        foreach (var other in hitEnemies)
        {
            if (other != null)
            {
                IEnemy ec = other.transform.GetComponent<IEnemy>();
                StartCoroutine(ec.Hurt(damage, knockbackForce));
                // Debug.Log("Hurt " + other.name + "!");
            }
        }

        float diameter = radius * 2;
        attackType.transform.localScale = new Vector3(diameter, diameter, 1f);
        GameObject attack = Instantiate(attackType, sendRange, Quaternion.identity);
        
        yield return new WaitForSeconds(attackSpriteShowTime);
        
        Destroy(attack);
        
        attacking = false;
    }

    public IEnumerator TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Debug.Log("Died!");
            // start game over screen
        }
        
        mat.color = Color.red;
        
        yield return new WaitForSeconds(IFrameTime);

        mat.color = originalColor;
    }

    public void UpdateStats()
    {
        speed = stats.moveSpeed;
        damage = stats.attackDamage;
        range = stats.attackRange;
        attackSpeed = stats.attackSpeed;
        knockbackForce = stats.knockbackCoefficient;
    }
}