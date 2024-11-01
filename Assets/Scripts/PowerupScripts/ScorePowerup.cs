using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePowerup : MonoBehaviour
{
    private PullInteraction _pullInteraction;
    void Start()
    {
        _pullInteraction = GameObject.Find("BowString").GetComponent<PullInteraction>();
    }

    public void ScorePowerupHit()
    {
        Debug.Log("Score Powerup was hit");
        UIManager.Instance.ReceiveScorePowerup();
        Destroy(gameObject);
    }
}
