using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AudioSource _hitSFX;

    private float _forceValue;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _hitSFX = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_forceValue >= 1f)
        {
            Debug.Log("Target Trigger was hit! Force Value is: " + _forceValue);

            _hitSFX.Play(); // May create an array to make a variety of SFX
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            StartCoroutine(DestroyTargetOverTime());
        }

    }

    private IEnumerator DestroyTargetOverTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void UpdateForceValue(float value)
    {
        _forceValue = value;
    }
}
