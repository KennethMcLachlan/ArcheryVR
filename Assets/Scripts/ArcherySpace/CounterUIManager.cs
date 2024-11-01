using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterUIManager : MonoBehaviour
{
    private static CounterUIManager _instance;
    public static CounterUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Counter UI Manager Instance is Null");
            }
            return _instance;
        }
    }

    [SerializeField] private ArcheryGameManager _gameManager;
    [SerializeField] private TMP_Text _aText;

    private float _one = 1f;
    private bool _isFading;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        _isFading = true;
    }

    void Update()
    {
        if (_isFading == true)
        {
            _aText.CrossFadeAlpha(0f, 0.05f, false);
        }
        else
        {
            _aText.CrossFadeAlpha(1.0f, 0.5f, false);
        }
    }

    public void ATextFadeOut()
    {
        _isFading = true;
    }

    public void ATextFadeIn()
    {
        _isFading = false;
    }

    
}
