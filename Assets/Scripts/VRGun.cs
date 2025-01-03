using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class VRGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;

    private XRBaseInteractor currentInteractor; // Stores the interactor grabbing the gun
    private bool isLeftHand; // Tracks whether the left hand is grabbing the gun

    private void Awake()
    {
        // Attach event handlers
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        // Detach event handlers to avoid memory leaks
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Get the interactor (hand/controller) grabbing the gun
        currentInteractor = (XRBaseInteractor)args.interactorObject;

        // Check if it's the left or right hand based on the controller's name or properties
        if (currentInteractor.transform.name.ToLower().Contains("left"))
        {
            isLeftHand = true;
            Debug.Log("Gun grabbed with left hand.");
        }
        else if (currentInteractor.transform.name.ToLower().Contains("right"))
        {
            isLeftHand = false;
            Debug.Log("Gun grabbed with right hand.");
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Reset the interactor when the gun is released
        if (args.interactorObject == currentInteractor)
        {
            currentInteractor = null;
            Debug.Log("Gun released.");
        }
    }

    public void Shoot()
    {
        if (currentInteractor == null)
        {
            Debug.LogWarning("No hand is grabbing the gun. Cannot shoot!");
            return;
        }

        // Instantiate and shoot the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;

        Debug.Log(isLeftHand ? "Shooting with left hand!" : "Shooting with right hand!");
    }
}
