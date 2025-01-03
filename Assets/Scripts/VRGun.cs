using UnityEngine;
using UnityEngine.InputSystem;

public class VRGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // Bullet prefab to instantiate
    [SerializeField] private Transform firePoint; // Fire point of the gun
    [SerializeField] private float bulletSpeed = 20f; // Speed of the bullet
    [SerializeField] private InputActionReference shootAction; // VR controller trigger action

    private void OnEnable()
    {
        shootAction.action.performed += OnShoot;
    }

    private void OnDisable()
    {
        shootAction.action.performed -= OnShoot;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Shoot();
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Bullet prefab or fire point not assigned!");
            return;
        }

        // Instantiate and fire the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;

        // Debug or add effects
        Debug.Log("Bullet fired!");
    }
}
