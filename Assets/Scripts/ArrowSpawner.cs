using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;
    public GameObject bombArrow;
    public GameObject notch;

    private XRGrabInteractable _bow;
    private GameObject _currentArrow = null;

    private bool _arrowNotched;
    void Start()
    {
        _bow = GetComponent<XRGrabInteractable>();
        PullInteraction.PullActionReleased += NotchEmpty; 
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= NotchEmpty;
    }
    
    void Update()
    {
        if (_bow.isSelected && _arrowNotched == false)
        {
            _arrowNotched = true;
        }
        if (!_bow.isSelected && _currentArrow != null)
        {
            NotchEmpty(1f);
        }
    }

    private void NotchEmpty(float value)
    {
        _arrowNotched = false;
        _currentArrow = null;
    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(1f);
        _currentArrow = Instantiate(arrow, notch.transform);
    }

    public void InstantiateArrow()
    {
        _currentArrow = Instantiate(arrow, notch.transform);
    }

    public void InstantiateBombArrow()
    {
        _currentArrow = Instantiate(bombArrow, notch.transform);
    }
}
