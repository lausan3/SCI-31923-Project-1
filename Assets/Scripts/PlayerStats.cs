using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float moveSpeed = 10f;
    public float attackDamage = 10f;
    public float attackRange = 3f;
    public float attackSpeed = 2f;
    public float knockbackCoefficient = 5f;

    public void ResetStats()
    { 
        maxHealth = 100f;
        moveSpeed = 10f;
        attackDamage = 20f;
        attackRange = 5f;
        attackSpeed = 1.8f;
    }
}
