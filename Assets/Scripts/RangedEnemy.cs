using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [SerializeField] private GameObject projectilePrefab;  // Projectile prefab
    [SerializeField] private Transform firePoint;  // Fire point where projectiles will be launched
    [SerializeField] private float attackRate = 1f;  // Time between attacks
    [SerializeField] private float projectileSpeed = 10f;  // Speed of the projectile

    private float lastAttackTime;  // Keeps track of last attack time

    protected override void Start()
    {
        base.Start();  // Call the base class Start to initialize enemy data
        lastAttackTime = Time.time;  // Initialize the last attack time
    }

    protected override void Update()
    {
        base.Update();  // Call base Update for any generic behavior

        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // If within attack range, stop moving and attack
        if (distanceToPlayer <= attackRange)
        {
            // Stop moving and start attacking
            StopMovingAndAttack();
        }
        else
        {
            // Move towards the player if out of attack range
            MoveTowardsPlayer();
        }
    }

    // Stop moving and shoot the projectile
    private void StopMovingAndAttack()
    {
        // Check if enough time has passed to shoot
        if (Time.time - lastAttackTime >= attackRate)
        {
            ShootProjectile();
        }
    }

    // Method to shoot the projectile
    private void ShootProjectile()
    {
        // Instantiate the projectile at the fire point and set its direction
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = (playerTransform.position - transform.position).normalized * projectileSpeed;
        }

        // Add damage logic directly to the projectile (we'll check for collision and apply damage in the projectile's behavior)
        projectile.GetComponent<Projectile>().damage = attackDamage;

        lastAttackTime = Time.time;  // Update the last attack time
    }

    // Override MoveTowardsPlayer in RangedEnemy to ensure proper movement towards the player
    protected override void MoveTowardsPlayer()
    {
        if (health > 0)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }
}
