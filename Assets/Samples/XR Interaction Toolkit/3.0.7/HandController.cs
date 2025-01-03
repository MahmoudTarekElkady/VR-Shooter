using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class HandController : MonoBehaviour
{
    [SerializeField] private InputActionReference _tirggerValue;
    [SerializeField] private InputActionReference _grabValue;
    [SerializeField] private XRPokeInteractor pokeInteractor;
    private Animator _handAnimator;
    private bool isHovering;

    private void OnEnable()
    {
        _handAnimator = GetComponent<Animator>();
        pokeInteractor.uiHoverEntered.AddListener(OnHoverEnter);
        pokeInteractor.uiHoverExited.AddListener(OnHoverExist);
    }

    private void OnDisable()
    {
        pokeInteractor.uiHoverEntered.RemoveListener(OnHoverEnter);
        pokeInteractor.uiHoverExited.RemoveListener(OnHoverExist);
    }

    private void Update()
    {
        if (isHovering)
        {
            _handAnimator.SetFloat("Grip", 1);
            _handAnimator.SetFloat("Trigger", 0);
            return;
        }

        if (_grabValue)
        {
            float value = _grabValue.action.ReadValue<float>();
            _handAnimator.SetFloat("Grip", value);

        }

        if (_tirggerValue)
        {
            float value = _tirggerValue.action.ReadValue<float>();
            _handAnimator.SetFloat("Trigger", value);
        }
    }
    private void OnHoverEnter(UIHoverEventArgs arg0)
    {
        isHovering = true;
        _handAnimator.SetFloat("Grip", 1);
        _handAnimator.SetFloat("Trigger", 0);
    }

    private void OnHoverExist(UIHoverEventArgs arg0)
    {
        isHovering = false;
        _handAnimator.SetFloat("Grip", 0);
        _handAnimator.SetFloat("Trigger", 0);
    }
}
