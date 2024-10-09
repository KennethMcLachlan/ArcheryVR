using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ControllerInput : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _pauseMenu;
    public InputActionReference openPauseMenuAction;

    private void Awake()
    {
        openPauseMenuAction.action.Enable();
        openPauseMenuAction.action.performed += ToggleMenu;
        InputSystem.onDeviceChange += onDeviceChange;
    }

    private void OnDestroy()
    {
        openPauseMenuAction.action.Disable();
        openPauseMenuAction.action.performed -= ToggleMenu;
        InputSystem.onDeviceChange -= onDeviceChange;
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        //Call on fade out
        //Reposition the Player to the Pause area
        //Enable UI Buttons
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        
    }

    //Can be useful when changing from controllers to hand tracking
    private void onDeviceChange(InputDevice device, InputDeviceChange change) //May not be neccessary for this project
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                openPauseMenuAction.action.Disable();
                openPauseMenuAction.action.performed -= ToggleMenu;
                break;
            case InputDeviceChange.Reconnected:
                openPauseMenuAction.action.Enable();
                openPauseMenuAction.action.performed += ToggleMenu;
                break;

        }
    }

    private void Update()
    {
        //Enable Fade Out / Fade In
    }


}
