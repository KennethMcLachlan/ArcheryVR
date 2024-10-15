using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    [SerializeField] private AudioSource _hitSFX;

    private float _forceValue;
    private int _score;

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
        UIManager.Instance.UpdateScore();
        _hitSFX.Play();
        StartCoroutine(DestroyTargetOverTime());
    }
    
    private IEnumerator UpdateScoreRoutine()
    {
        yield return new WaitForEndOfFrame();
        UIManager.Instance.UpdateScore();

    }
}
