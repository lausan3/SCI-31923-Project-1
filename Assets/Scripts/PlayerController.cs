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

    private float health;
    private float damage;
    private float range;
    private float attackSpeed;
    private float knockbackForce;
    private Vector2 aimingDirection;
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
        if (Input.GetKey("W"))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.up;
        }

        if (Input.GetKey("A"))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.left;
        }

        if (Input.GetKey("S"))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.down;
        }

        if (Input.GetKey("D"))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.right;
        }
    }

    private IEnumerator Attack(float waitTime)
    {
        attacking = true;
        
        // check for overlap
        Collider2D[] hit = Physics2D.OverlapCapsuleAll(transform.position * aimingDirection * range,
             Vector2.one * range, 
            0f,
            enemyMask);
        
        Debug.DrawRay(Vector3.one * range, aimingDirection, Color.green);

        foreach (var other in hit)
        {
            EnemyController enemy = other.transform.GetComponent<EnemyController>();
            StartCoroutine(enemy.Hurt(damage));
        }
        
        yield return new WaitForSeconds(waitTime);

        attacking = false;
    }

    public void UpdateStats()
    {
        damage = stats.attackDamage;
        range = stats.attackRange;
        attackSpeed = stats.attackSpeed;
        knockbackForce = stats.knockbackCoefficient;
    }
}
