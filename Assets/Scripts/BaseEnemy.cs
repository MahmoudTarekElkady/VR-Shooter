using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public EnemyData enemyData;  // Reference to the ScriptableObject holding the enemy data

    protected float health;
    protected float speed;
    protected int scoreValue;
    protected Transform playerTransform;
    protected float attackRange;  // Attack range for triggering attack
    protected float attackDamage;

    protected virtual void Start()
    {
        // Initialize from ScriptableObject data
        health = enemyData.health;
        speed = enemyData.speed;
        scoreValue = enemyData.scoreValue;
        attackRange = enemyData.attackRange;
        attackDamage = enemyData.attackDamage;
        playerTransform = GameObject.FindWithTag("Player").transform; // Get reference to the player
    }

    protected virtual void Update()
    {
        // Ensure the enemy stays above the ground
        KeepAboveGround();
    }

    // Move towards the player if in range
    protected virtual void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if ( distanceToPlayer <= attackRange && health > 0)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    // Ensure the enemy stays above the ground
    private void KeepAboveGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            Vector3 newPosition = transform.position;
            newPosition.y = hit.point.y + 1f;  // Adjust based on collider height
            transform.position = newPosition;
        }
    }

    // Handle damage to the enemy
    public virtual void TakeDamage(float damage)
    {
        if (health <= 0) return;  // Prevent further damage after death

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
}
