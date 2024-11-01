using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PullInteraction : XRBaseInteractable
{
    public static event Action<float> PullActionReleased;

    public Transform start, end;
    public GameObject notch;

    public float pullAmount { get; private set; } = 0.0f;
    private LineRenderer _lineRenderer;

    private IXRSelectInteractor _pullingInteractor = null;
    private ArrowSpawner _arrowSpawner;
    private AudioSource _audioSource;

    private bool _bombArrowIsActive;
    private bool _arrowIsSpawned;

    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _arrowSpawner = GameObject.Find("Bow").GetComponent<ArrowSpawner>();
    }

    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        _pullingInteractor = args.interactorObject;
    }

    public void Release()
    {
        if (PullActionReleased != null)
        {
            PullActionReleased?.Invoke(pullAmount);
            _pullingInteractor = null;
            pullAmount = 0f;

            notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, 0f);
            UpdateString();

            PlayReleaseAudio();
            _arrowIsSpawned = false;
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {

            if (isSelected )
            {
                if (_bombArrowIsActive == true && _arrowIsSpawned == false)
                {
                    _arrowSpawner.InstantiateBombArrow();
                }
                else if (_bombArrowIsActive == false && _arrowIsSpawned == false)
                {
                    _arrowSpawner.InstantiateArrow();
                }

                _arrowIsSpawned = true;
                Vector3 pullPosition = _pullingInteractor.transform.position;
                pullAmount = CalculatePull(pullPosition);

                UpdateString();
            }

        }
    }

    public float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }

    private void UpdateString()
    {
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(start.transform.localPosition.z, end.transform.localPosition.z, pullAmount);

        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, linePosition.z + 0.2f);
        _lineRenderer.SetPosition(1, linePosition);
    }

    private void PlayReleaseAudio()
    {
        _audioSource.Play();
    }

    private IEnumerator BombDurationRoutine()
    {
        yield return new WaitForSeconds(5f);
        _bombArrowIsActive = false;
    }

    public void ReceiveBombInfoTrue()
    {
        _bombArrowIsActive = true;
        StartCoroutine(BombDurationRoutine());
    }

    public void ReceiveBombInfoFalse()
    {
        _bombArrowIsActive = false;
    }
}
