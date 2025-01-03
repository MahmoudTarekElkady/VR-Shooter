using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;  // Player's health
    public float maxHealth = 100f;  // Max health

    // Method to take damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Handle player death
    private void Die()
    {
        // Optional: Add death animations, sound, or effects
        Debug.Log("Player has died!");
        // You can trigger a GameOver event or restart the level
    }

    // Optional: Heal the player
    public void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }
}
