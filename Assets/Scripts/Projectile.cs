using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;  // Damage dealt by the projectile

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the projectile hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Destroy the projectile after it hits the player
            Destroy(gameObject);
        }
    }
}
