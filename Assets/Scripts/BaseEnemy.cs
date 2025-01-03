using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public EnemyData enemyData;  // Reference to the ScriptableObject holding the enemy data

    protected float health;
    protected float speed;
    protected int scoreValue;
    protected Transform playerTransform;
    protected float attackRange;  // Attack range for triggering attack
    protected float attackDamage; // Attack damage
    protected GameObject projectilePrefab; // For ranged attacks
    protected float fireRate;  // Fire rate for ranged attack
    protected bool isRangedEnemy; // Whether the enemy is ranged or melee

    protected virtual void Start()
    {
        // Initialize from ScriptableObject data
        health = enemyData.health;
        speed = enemyData.speed;
        scoreValue = enemyData.scoreValue;
        attackRange = enemyData.attackRange;
        attackDamage = enemyData.attackDamage;
        projectilePrefab = enemyData.projectilePrefab;
        fireRate = enemyData.fireRate;

        isRangedEnemy = projectilePrefab != null;

        playerTransform = GameObject.FindWithTag("Player").transform; // Get reference to the player
    }

    protected virtual void Update()
    {
        MoveTowardsPlayer();

        // Check if within attack range and attack
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            Attack();
        }
    }

    // Move towards the player if in range
    protected virtual void MoveTowardsPlayer()
    {
        if (health > 0)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    // Handle damage to the enemy
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Handle enemy death
    protected void Die()
    {
        // Optional: Add death animations, sound, or effects
        GameManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }

    // Perform attack based on enemy type (melee or ranged)
    protected virtual void Attack()
    {
        if (isRangedEnemy)
        {
            // Handle ranged attack by shooting a projectile
            ShootProjectile();
        }
        else
        {
            // Handle melee attack logic
            MeleeAttack();
        }
    }

    // Ranged attack by shooting a projectile
    protected void ShootProjectile()
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                rb.velocity = direction * fireRate; // Fire at the player with the given speed
            }
        }
    }

    // Melee attack logic (add actual implementation as needed)
    protected void MeleeAttack()
    {
        Debug.Log($"{enemyData.enemyName} is attacking!");
        // Implement damage logic (e.g., reduce player health)
    }
}
