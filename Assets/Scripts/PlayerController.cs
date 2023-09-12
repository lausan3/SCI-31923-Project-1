using System.Collections;
using Unity.Mathematics;
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

    public float overlapRadius = 0.5f;
    public GameObject attackType;

    private float health;
    private float damage;
    private float range;
    private float attackSpeed;
    private float knockbackForce;
    private Vector2 aimingDirection = Vector2.right;
    private bool attacking;
    private float timeToAttackSecs;

    private float timer;

    private void Start()
    {
        UpdateStats();
        attacking = false;
        timer = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        timer -= Time.deltaTime;

        if (timer <= 0.005 && !attacking)
        {
            StartCoroutine(Attack(0.05f));
            timer = attackSpeed;
        }
    }

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.right;
        }
        
        Debug.Log((aimingDirection));
    }

    private IEnumerator Attack(float waitTime)
    {
        attacking = true;

        // Physics2D.OverlapCircleAll()
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(aimingDirection * range, overlapRadius, enemyMask);

        foreach (var other in hitEnemies)
        {
            if (other != null)
            {
                IEnemy ec = other.transform.GetComponent<IEnemy>();
                ec.Hurt(damage);
            }
        }
        
        GameObject attack = Instantiate(attackType, transform.position * range * aimingDirection, Quaternion.identity);
        
        yield return new WaitForSeconds(waitTime);
        
        Destroy(attack);
        
        attacking = false;
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
