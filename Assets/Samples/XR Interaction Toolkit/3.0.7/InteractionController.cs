using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private bool useTeleport = false;
    //[SerializeField] private bool useGrabbing = false;
    [SerializeField] private bool useRotation = false;
    [SerializeField] private bool useMovements;

    public DynamicMoveProvider movementScript;
    public SnapTurnProvider rotationScript;
    public TeleportationProvider teleportationScript;

    [Tooltip("The interactor used for teleportation.")]
    [SerializeField] public GameObject teleportIndicator;

    private GameObject xrOrigin;


    private void Awake()
    {
        if (teleportationScript == null)
            teleportationScript = FindObjectOfType<TeleportationProvider>();

        if (movementScript == null)
           movementScript = FindObjectOfType<DynamicMoveProvider>();

        if (rotationScript == null)
            rotationScript = FindObjectOfType<SnapTurnProvider>();

        //if (grabbingScript == null)
        //    grabbingScript = FindObjectOfType<GrabMoveProvider>();

        xrOrigin = FindObjectOfType<XROrigin>().gameObject;
        Setup();
    }

    private void Setup()
    {

        SetupTeleport();
        SetupRotation();
        //SetupGrabbing();
    }

    private void SetupTeleport()
    {
        if (teleportationScript != null)
        {
            teleportationScript.enabled = useTeleport;
            teleportationScript.gameObject.SetActive(useTeleport);
        }
        if (teleportIndicator != null)
        {
            Transform rightController = transform.Find("XR Origin (XR Rig) Variant/Camera Offset/Right Controller"); // Assuming "Right Controller" is the name of the parent GameObject
            if (rightController != null)
            {
                teleportIndicator = rightController.Find("Teleport Interactor")?.gameObject;
                if (teleportIndicator != null)
                {
                    teleportIndicator.SetActive(useTeleport);
                    XRRayInteractor interactorRight = teleportIndicator.GetComponent<XRRayInteractor>();
                    if (interactorRight != null)
                    {
                        interactorRight.enabled = useTeleport; // Enable if teleportation is on
                        Debug.Log("XRRayInteractor found and enabled.");
                    }
                    else
                    {
                        Debug.Log("XRRayInteractor component not found on teleportIndicator.");
                    }
                }
                else
                {
                    Debug.Log("Right Teleport Interactor GameObject not found under Right Controller.");
                }
            }
            else
            {
                Debug.Log("Right Controller GameObject not found.");
            }
        }
        else
        {
            Debug.Log("teleportIndicator is null.");
        }
    }

    private void SetupMovement()
    {
        if (movementScript != null)
        {
            movementScript.enabled = useMovements;
            movementScript.gameObject.SetActive(useMovements);
        }
    }

    private void SetupRotation()
    {
        if (rotationScript != null)
        {
            rotationScript.enabled = useRotation;
            rotationScript.gameObject.SetActive(useRotation);
        }
    }

    //private void SetupGrabbing()
    //{
    //    if (grabbingScript != null)
    //    {
    //        grabbingScript.enabled = useGrabbing;
    //        grabbingScript.gameObject.SetActive(useGrabbing);
    //    }
    //}

    public void ChangePlayerOrienation(Quaternion quaternion)
    {
        xrOrigin.transform.localRotation = quaternion;
    }
    public void ChangePlayerPostion(Vector3 postion)
    {
        xrOrigin.transform.localPosition = postion;
    }
    //setters

    public void SetTeleport(bool value)
    {
        this.useTeleport = value;
        SetupTeleport();
    }

    public void SetMovement(bool value)
    {
        useMovements = value;
        SetupMovement();
    }

    public void SetRotation(bool value)
    {
        useRotation = value;
        SetupRotation();
    }

    //public void SetGrabbing(bool value)
    //{
    //    this.useGrabbing = value;
    //    SetupGrabbing();

    //}

    public enum InteractionsType
    {
        Teleport,
        Grabbing,
        movement,
        rotation
    }

    public enum RotationType
    {
        Snapping,
        Continus
    }
}