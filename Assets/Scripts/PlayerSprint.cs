using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerSprint : MonoBehaviour
{
    [SerializeField] private ActionBasedContinuousMoveProvider _playerMovement;

    public InputActionReference aButton;

    private void Awake()
    {
        _playerMovement = GetComponent<ActionBasedContinuousMoveProvider>();

        aButton.action.Enable();
        aButton.action.performed += SprintStarted;
        aButton.action.canceled += SprintCanceled;
    }

    private void SprintCanceled(InputAction.CallbackContext context)
    {
        _playerMovement.moveSpeed = 5;
    }

    private void SprintStarted(InputAction.CallbackContext context)
    {
        _playerMovement.moveSpeed = 9;
    }


    private void OnDestroy()
    {
        aButton.action.performed -= SprintStarted;
        aButton.action.canceled -= SprintCanceled;
        aButton.action.Disable();
    }
}
