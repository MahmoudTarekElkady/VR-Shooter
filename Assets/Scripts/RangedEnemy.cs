using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    private GameObject projectilePrefab;  // Projectile prefab
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

        // Check if the player is in attack range and if enough time has passed to shoot
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= enemyData.attackRange && Time.time - lastAttackTime >= attackRate)
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
        rb.velocity = (playerTransform.position - transform.position).normalized * projectileSpeed;

        lastAttackTime = Time.time;  // Update the last attack time
    }
}
