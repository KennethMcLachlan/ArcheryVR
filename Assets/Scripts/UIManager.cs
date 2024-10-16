using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is null");
            }
            return _instance;
        }
    }

    //Timer
    [SerializeField] private float _timeRemaining;
    [SerializeField] private bool _timerIsActive;
    [SerializeField] private TMP_Text _timerText;

    //Score
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private int _scoreValue;

    //ScorePowerup
    private bool _scorePowerupIsActive;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _scoreText.text = "Score: ";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _timeRemaining = 90f;
            _timerIsActive = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ReceiveExtraTime();
        }

        if (_timerIsActive == true)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                UpdateTimer(_timeRemaining);
            }
            else if (_timeRemaining <= 0)
            {
                Debug.Log("Out of time");
                _timerIsActive = false;
            }
        }
    }

    //Update the timer text
    public void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        _timerText.text = string.Format("{0:0} : {1:00}", minutes, seconds);
    }

    //Update Score from Target Hits
    public void UpdateScore()
    {
        if (_scorePowerupIsActive == true)
        {
            _scoreValue += 100 * 2;
            StartCoroutine(ScorePowerupCooldownRoutine());
        }
        else
        {
            _scoreValue += 100;
        }
        _scoreText.text = "Score: " + _scoreValue.ToString();
        Debug.Log("Current Score is" +  _scoreValue);
    }

    //Score Powerup
    private IEnumerator ScorePowerupCooldownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _scorePowerupIsActive = false;
    }

    public void ReceiveScorePowerup()
    {
        _scorePowerupIsActive = true;
    }

    //Timer Powerup

    public void ReceiveExtraTime()
    {
        _timeRemaining += 15f;
    }

}
