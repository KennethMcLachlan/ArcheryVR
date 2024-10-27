using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarmentSpeechTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _garmentSpeech;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _garmentSpeech.Play();
        }
    }
}
