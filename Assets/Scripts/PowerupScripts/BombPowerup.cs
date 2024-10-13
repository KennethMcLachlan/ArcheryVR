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

    void Update()
    {
        
    }

    public void BombPowerupHit()
    {
        Debug.Log("BombPowerup was hit");
        pullInteraction.ReceiveBombInfoTrue();
        //Play SFX here
        Destroy(gameObject);
    }
}
