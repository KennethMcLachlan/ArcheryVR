using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private Image _blackOverlay;
    [SerializeField] private bool _fadeIsActive;
    private bool _isFadingToBlack;

    public void FadeOutStart()
    {
        var tempAlpha = _blackOverlay.color;
        tempAlpha.a -= 0.5f * Time.deltaTime;
        _blackOverlay.color = tempAlpha;
    }

    private void FadeToBlackStart()
    {
        var tempAlpha = _blackOverlay.color;
        tempAlpha.a += 0.5f * Time.deltaTime;
        _blackOverlay.color = tempAlpha;
    }

    private IEnumerator FadeTimingCoroutine() 
    {
        FadeToBlackStart();
        yield return new WaitForSeconds(1.0f);
        FadeOutStart();


    }
}
