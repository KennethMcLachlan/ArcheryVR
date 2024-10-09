using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    [SerializeField] private AudioSource _hitSFX;

    private float _forceValue;

    private void Start()
    {
        _hitSFX = GetComponent<AudioSource>();
    }

    private IEnumerator DestroyTargetOverTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void TargetHit()
    {
        //Add Score when implemented
        Debug.Log("Target was hit and communicated from Arrow Script");
        _hitSFX.Play();
        StartCoroutine(DestroyTargetOverTime());
    }
}
