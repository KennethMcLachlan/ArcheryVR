using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    private float _forceValue;
    private int _score;

    private IEnumerator DeactivateTargetOverTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void TargetHit()
    {
        Debug.Log("Target was hit and communicated from Arrow Script");
        UIManager.Instance.UpdateScore();
        StartCoroutine(DeactivateTargetOverTime());
    }
    
    private IEnumerator UpdateScoreRoutine()
    {
        yield return new WaitForEndOfFrame();
        UIManager.Instance.UpdateScore();

    }
}
