using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    private bool isAttacking = false;

    protected override void Start()
    {
        base.Start();  // Initialize the base enemy

        // Initialize melee-specific values (if needed)
    }

    protected override void Update()
    {
        base.Update();  // Ensure common logic from BaseEnemy is called

        if (health <= 0) return;

        // Move towards the player
        MoveTowardsPlayer();

        // Check if in attack range to start melee attack
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange && !isAttacking)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        isAttacking = true;
        // Trigger melee attack (e.g., animation or damage)
        Debug.Log($"{enemyData.enemyName} attacks the player!");
        // Apply damage to the player
        playerTransform.GetComponent<PlayerHealth>().TakeDamage(enemyData.attackDamage);
        isAttacking = false;
    }
}
