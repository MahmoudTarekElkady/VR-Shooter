using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    public EnemyData enemyData;  // Reference to the ScriptableObject

    private bool isAttacking = false;

    protected override void Start()
    {
        base.Start();

        // Initialize from ScriptableObject
        health = enemyData.health;
        speed = enemyData.speed;
        scoreValue = enemyData.scoreValue;
        attackRange = enemyData.attackRange;
        attackDamage = enemyData.attackDamage;
    }

    protected void Update()
    {
        if (health <= 0) return;

        // Move towards the player
        MoveTowardsPlayer();

        // Check if in attack range to start melee attack
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange && !isAttacking)
        {
            AttackPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        // Move enemy toward player
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void AttackPlayer()
    {
        isAttacking = true;
        // Trigger melee attack (e.g., animation or damage)
        Debug.Log($"{enemyData.enemyName} attacks the player!");
        // Apply damage to the player
        playerTransform.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        isAttacking = false;
    }
}
