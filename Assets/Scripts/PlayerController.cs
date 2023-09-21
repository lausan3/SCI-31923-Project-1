using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Scripting;

public class PlayerController : MonoBehaviour
{
    // TODO: Change targetting to be based on floats instead of cardinal directions, makes it more fun or mouse honestly
    [RequiredMember]
    public PlayerStats stats;
    
    [Header("Speed")] 
    private float speed;

    [Header("Combat")] 
    public LayerMask enemyMask;

    public float overlapRadius = 0.5f;
    public GameObject attackType;
    public Transform aimIndicator;

    private float health;
    private float damage;
    // this should be a total range from transform.pos -> the end of the overlap circle
    private float range;
    private float attackSpeed;
    private float knockbackForce;
    private Vector2 aimingDirection = Vector2.right;
    private bool attacking;
    private float timeToAttackSecs;
    private float arrowRotation = 0f;

    private float timer;
    private CircleCollider2D coli;

    [Header("Debug")] public float debugLineZ = 0f;

    private void Start()
    {
        UpdateStats();
        attacking = false;
        timer = attackSpeed;
        coli = GetComponent<CircleCollider2D>();
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
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.up;
            arrowRotation = 90f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.left;
            arrowRotation = 180f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.down;
            arrowRotation = 270f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            // set aiming angle
            aimingDirection = Vector2.right;
            arrowRotation = 0f;
        }
        
        // aiming indicator
        aimIndicator.position = new Vector3(transform.position.x + (aimingDirection.x * 1f),transform.position.y + (aimingDirection.y * 1f), 1f);
        Quaternion aimRotation = quaternion.EulerXYZ(0f, 0f, Mathf.Deg2Rad * arrowRotation);
        aimIndicator.transform.rotation = aimRotation;

        // Debug.Log((aimingDirection));
    }

    private IEnumerator Attack(float waitTime)
    {
        attacking = true;

        // Physics2D.OverlapCircleAll()
        float radius = (range - coli.radius) / 2;
        // this is the range to use for OverlapCircleAll
        Vector2 sendRange = (Vector2)transform.position + (aimingDirection * range) - (aimingDirection * new Vector2(radius, radius));
        // Debug.Log("Current aim dir * range: " + aimingDirection * range);
        // Debug.Log("Current position: " + transform.position);
        // Debug.Log("Send range: " + sendRange);
        // Debug.Log("Circle radius: " + radius);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(sendRange, radius, enemyMask);
        // Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, debugLineZ), new Vector3(transform.position.x + range, transform.position.y + range, debugLineZ), Color.red);
        // Debug.Log("Attacked!");
        
        foreach (var other in hitEnemies)
        {
            if (other != null)
            {
                IEnemy ec = other.transform.GetComponent<IEnemy>();
                StartCoroutine(ec.Hurt(damage));
                // Debug.Log("Hurt " + other.name + "!");
            }
        }

        float diameter = radius * 2;
        attackType.transform.localScale = new Vector3(diameter, diameter, 1f);
        GameObject attack = Instantiate(attackType, sendRange, Quaternion.identity);
        // Debug.Log(attack.transform.position);
        // Debug.Log(sendRange);
        
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
