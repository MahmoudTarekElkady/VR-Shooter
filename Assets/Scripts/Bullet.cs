using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Handle collision logic
        Destroy(gameObject);
    }

    void Start()
    {
        Destroy(gameObject, 5f); // Destroy after 5 seconds
    }
}
