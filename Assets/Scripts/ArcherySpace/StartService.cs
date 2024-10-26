using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartService : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            CounterUIManager.Instance.ATextFadeIn();
            //Disable Sprint "when Sprint is implemented"
            //so the "A" Button can initiate the counter

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player"))
        {
            CounterUIManager.Instance.ATextFadeOut();
        }
    }
}
