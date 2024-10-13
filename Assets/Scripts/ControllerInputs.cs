using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInputs : MonoBehaviour
{
    public InputActionReference bButton;
    private bool _rayIsActive;
    [SerializeField] private GameObject _rayInteractor;

    private void Awake()
    {
        bButton.action.Enable();
        bButton.action.performed += bButtonPerformed;
    }

    private void bButtonPerformed(InputAction.CallbackContext context)
    {
        _rayIsActive = !_rayIsActive;

        if (_rayIsActive == true)
        {
            _rayInteractor.SetActive(true);
        }
        else
        {
            _rayInteractor.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        bButton.action.Disable();
        bButton.action.performed -= bButtonPerformed;
    }
}
