using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerup : MonoBehaviour
{
    private PullInteraction _pullInteraction;

    private void Start()
    {
        _pullInteraction = GameObject.Find("BowString").GetComponent<PullInteraction>();
    }

    public void TimePowerupHit()
    {
        Debug.Log("TimePowerup was Hit");
        UIManager.Instance.ReceiveExtraTime();
        Destroy(gameObject);
    }
}
