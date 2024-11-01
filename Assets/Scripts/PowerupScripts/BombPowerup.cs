using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerup : MonoBehaviour
{
    private PullInteraction pullInteraction;
    void Start()
    {
        pullInteraction = GameObject.Find("BowString").GetComponent<PullInteraction>();
    }

    public void BombPowerupHit()
    {
        pullInteraction.ReceiveBombInfoTrue();
        Destroy(gameObject);
    }
}
