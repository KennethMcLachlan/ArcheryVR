using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{


    [SerializeField] private Transform _player;
    private Vector3 _activePlayPosition;
    private Quaternion _activePlayRotation;
    [SerializeField] private Transform _pauseSpawnLocation;

    [SerializeField] private GameObject _pauseMenu;
    public InputActionReference openPauseMenuAction;

    private bool _pauseMenuIsActive;

    //Fade In/Out variables
    //public VRFade _fadeManager;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f; // Duration for fade in/out

    private void Awake()
    {
        openPauseMenuAction.action.Enable();
        openPauseMenuAction.action.performed += ToggleMenu;
        InputSystem.onDeviceChange += onDeviceChange;

        StartCoroutine(GameStartFadeIn());
        _player = GameObject.Find("XR Origin (XR Rig)").GetComponent<Transform>();
        _pauseMenuIsActive = false;
    }

    private void OnDestroy()
    {
        openPauseMenuAction.action.Disable();
        openPauseMenuAction.action.performed -= ToggleMenu;
        InputSystem.onDeviceChange -= onDeviceChange;
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        _pauseMenuIsActive = !_pauseMenuIsActive; //Toggles Bool on Menu Button Press
        //_activePlayPosition.transform.position = _player.transform.position; //Cache to grab Player's gameplay position

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
        //_pauseMenu.SetActive(!_pauseMenu.activeSelf); ** Made from tutorial may not need
        
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

    private IEnumerator PauseBeginSequenceRoutine()
    {
        StartCoroutine(FadeToBlack());
        yield return new WaitForSeconds(1f);
        _player.position = _pauseSpawnLocation.position;
        _player.rotation = _pauseSpawnLocation.rotation;
        //footlock the player ***
        //Enable Controller Raycast Selector
        StartCoroutine(FadeOut());
    }

    private IEnumerator PauseEndSequenceRoutine()
    {
        StartCoroutine(FadeToBlack());
        yield return new WaitForSeconds(1f);
        _player.position = _activePlayPosition;
        _player.rotation = _activePlayRotation;
        //DisablePlayer Footlock
        //Disable Controller Raycast Selector
        StartCoroutine(FadeOut());
    }

    //Fade in at start of game (Helps ensure the Black Image is nopt present on start
    private IEnumerator GameStartFadeIn()
    {
        fadeImage.color = new Color(0, 0, 0, 1); // Start with black
        yield return FadeOut(); // Fade out from black
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
