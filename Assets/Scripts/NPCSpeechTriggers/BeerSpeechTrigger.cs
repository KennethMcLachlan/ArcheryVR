using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerSpeechTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _beerSpeech;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _beerSpeech.Play();
        }
    }
}
