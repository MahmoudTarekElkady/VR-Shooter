using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f; // Damage dealt by the bullet
    [SerializeField] private float lifetime = 5f; // Lifetime of the bullet before it gets destroyed
    [SerializeField] private float knockbackForce = 5f; // Force applied to knock back enemies

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after a set time
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hit an enemy
        if (collision.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
        {
            // Deal damage to the enemy
            enemy.TakeDamage(damage);

            // Apply knockback if the enemy has a Rigidbody
            Rigidbody enemyRb = collision.rigidbody;
            if (enemyRb != null)
            {
                Vector3 knockback = collision.relativeVelocity.normalized * knockbackForce;
                enemyRb.AddForce(knockback, ForceMode.Impulse);
            }
        }

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}
