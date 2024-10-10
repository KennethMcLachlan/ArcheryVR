using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    //[SerializeField] private Material _blackSphere;
    [SerializeField] private Image _blackOverlay;
    [SerializeField] private bool _fadeIsActive;
    private bool _isFadingToBlack;

    //[SerializeField] private DeviceBasedContinuousMoveProvider _playerMovement; //Accesses the Player Move Provider
    //public ActionBasedContinuousMoveProvider _player;

    private void Start()
    {
        //_playerMovement = GetComponent<DeviceBasedContinuousMoveProvider>(); //Accesses the Player Move Provider
        //_player = GetComponent<ActionBasedContinuousMoveProvider>();
    }
    private void Update()
    {
        //Test Input
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    _fadeIsActive = true;
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    _fadeIsActive = false;
        //}

        //if (_isFadingToBlack == true && _fadeIsActive == true)
        //{
        //    FadeToBlackStart();
        //}
        //else if (_isFadingToBlack == false && _fadeIsActive == true)
        //{
        //    FadeOutStart();
        //}

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(FadeTimingCoroutine());
        }
    }
    public void FadeOutStart()
    {
        //***Player movement may need to move to menu press script****
        //_playerMovement.moveSpeed = 0; // Prevents player movement when paused/when visuals are fading to black
        //_player.moveSpeed = 0f;

        //_fadeIsActive = true;

        var tempAlpha = _blackOverlay.color;
        tempAlpha.a -= 0.5f * Time.deltaTime;
        _blackOverlay.color = tempAlpha;

        //if (tempAlpha.a <= 0f) // Finishes Fading Out
        //{
        //    _fadeIsActive = false;
        //    _isFadingToBlack = false;
        //    //StartCoroutine(FadeTimingCoroutine());
        //    FadeOutStart();
        //}
    }

    private void FadeToBlackStart() //Fade to Black
    {
        var tempAlpha = _blackOverlay.color;
        tempAlpha.a += 0.5f * Time.deltaTime;
        _blackOverlay.color = tempAlpha;

        //if (tempAlpha.a >= 1f) // Screen is now black
        //{
        //    _fadeIsActive = false;
        //    _isFadingToBlack = false;
        //    //_player.moveSpeed = 6f;
        //}
    }

    private IEnumerator FadeTimingCoroutine() 
    {
        //Waits for a brief moment before fading back in

        FadeToBlackStart();
        //Reposition the player (XR Origin) to the Pause area
        yield return new WaitForSeconds(1.0f);
        FadeOutStart();


    }
}
