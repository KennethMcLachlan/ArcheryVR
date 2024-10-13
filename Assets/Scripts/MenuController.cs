using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class MenuController : MonoBehaviour
{

    //Player Positioning
    [SerializeField] private Transform _player;
    [SerializeField] private ActionBasedContinuousMoveProvider _playerMovement;
    private Vector3 _activePlayPosition;
    private Quaternion _activePlayRotation;
    [SerializeField] private Transform _pauseSpawnLocation;

    //Pause/Menu Button
    public InputActionReference openPauseMenuAction;
    private bool _pauseMenuIsActive;

    //Ray Interactor Object
    [SerializeField] private GameObject _rayInteractor;

    //Fade In/Out variables
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f; // Duration for fade in/out

    private void Awake()
    {
        openPauseMenuAction.action.Enable();
        openPauseMenuAction.action.performed += ToggleMenu;
        InputSystem.onDeviceChange += onDeviceChange;

        StartCoroutine(GameStartFadeIn());
        _player = GameObject.Find("XR Origin (XR Rig)").GetComponent<Transform>();
        _playerMovement = GameObject.Find("XR Origin (XR Rig)").GetComponent<ActionBasedContinuousMoveProvider>();
        if (_playerMovement == null)
        {
            Debug.Log("Move Provider has not been accessed");
        }
    }

    private void OnDestroy()
    {
        openPauseMenuAction.action.Disable();
        openPauseMenuAction.action.performed -= ToggleMenu;
        InputSystem.onDeviceChange -= onDeviceChange;
    }

    //Turns the menu on/off
    private void ToggleMenu(InputAction.CallbackContext context)
    {
        _pauseMenuIsActive = !_pauseMenuIsActive; //Toggles Bool on Menu Button Press

        if (_pauseMenuIsActive == true)
        {
            _activePlayPosition = _player.position;
            _activePlayRotation = _player.rotation;
            StartCoroutine(PauseBeginSequenceRoutine());
        }

        if (_pauseMenuIsActive == false)
        {
            StartCoroutine(PauseEndSequenceRoutine());
        }
    }

    // Fade from transparent to black
    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    // Fade from black to transparent
    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }
    }

    //Sequence of events that initiate when the Pause Button is active
    private IEnumerator PauseBeginSequenceRoutine()
    {
        _pauseMenuIsActive = true;
        StartCoroutine(FadeToBlack());
        yield return new WaitForSeconds(1f);
        _player.position = _pauseSpawnLocation.position;
        _player.rotation = _pauseSpawnLocation.rotation;
        _playerMovement.moveSpeed = 0f; //Prevents player movement when in menu
        _rayInteractor.SetActive(true); //Enable Controller Raycast Selector
        StartCoroutine(FadeOut());
    }

    //Sequence of Evenets that initiate to end the Pause Menu Area
    private IEnumerator PauseEndSequenceRoutine()
    {
        _pauseMenuIsActive = false;
        StartCoroutine(FadeToBlack());
        yield return new WaitForSeconds(1f);
        _player.position = _activePlayPosition;
        _player.rotation = _activePlayRotation;
        _playerMovement.moveSpeed = 5f; //Enables Player Movement
        _rayInteractor.SetActive(false); //Disable Controller Raycast Selector
        StartCoroutine(FadeOut());
    }

    //Fade in at start of game (Helps ensure the Black Image is not present on start)
    private IEnumerator GameStartFadeIn()
    {
        fadeImage.color = new Color(0, 0, 0, 1); // Start with black
        yield return FadeOut(); // Fade out from black
    }

    public void ResumeGameFromButton()
    {
        StartCoroutine(PauseEndSequenceRoutine());
    }

    //Used for Switching devices like controllers to and tracking (Error Prevention for this project)
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
}
