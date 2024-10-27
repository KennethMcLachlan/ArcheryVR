using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArcheryGameManager : MonoBehaviour
{
    //StillTarget Groups
    [SerializeField] private GameObject _targetGroupStill01;
    [SerializeField] private GameObject _targetGroupStill02;
    [SerializeField] private GameObject _targetGroupStill03;
    [SerializeField] private GameObject _targetGroupStill04;
    [SerializeField] private GameObject _targetGroupScatter;
    [SerializeField] private GameObject _targetGroupDownLeft01;
    [SerializeField] private GameObject _targetGroupDownLeft02;
    [SerializeField] private GameObject _targetGroupDownRight01;
    [SerializeField] private GameObject _targetGroupDownRight02;
    [SerializeField] private GameObject _targetGroupDownRight03;
    [SerializeField] private GameObject _targetGroupDiamondRight;
    [SerializeField] private GameObject _targetGroupDiamondLeft;
    [SerializeField] private GameObject _targetGroup3Stacks;
    [SerializeField] private GameObject _targetGroupPyramid;
    [SerializeField] private GameObject _targetGroupPyramidFlip;
    [SerializeField] private GameObject _targetGroupW;

    //Animated Target Groups
    [SerializeField] private GameObject _rainingTargets; //Finisher
    [SerializeField] private GameObject _topSideToSide;
    [SerializeField] private GameObject _middleSideToSide;
    [SerializeField] private GameObject _bottomSideToSide;
    [SerializeField] private GameObject _topOneWayRight;
    [SerializeField] private GameObject _middleOneWayRight;
    [SerializeField] private GameObject _bottomOneWayRight;
    [SerializeField] private GameObject _topOneWayLeft;
    [SerializeField] private GameObject _middleOneWayLeft;
    [SerializeField] private GameObject _bottomOneWayLeft;


    //WaitForSecondValues
    private float _one = 1f;
    private float _two = 2f;
    private float _three = 3f;
    private float _four = 4f;
    private float _five = 5f;
    private float _six = 6f;
    private float _seven = 7f;
    private float _eight = 8f;
    private float _nine = 9f;
    private float _ten = 10f;

    //CountDown Text
    [SerializeField] private TMP_Text _countdownText;

    //Game Start and End
    private Coroutine _gameCoroutine;
    private bool _gameIsActive;

    //Spawn/Despawn Powerups
    [SerializeField] private GameObject _powerupGroup;
    private GameObject _activePowerupGroup;

    //SFX
    [SerializeField] private GameObject _countdownSFX;
    [SerializeField] private GameObject _preReadySFX;
    [SerializeField] private GameObject _readyVO;
    [SerializeField] private GameObject _startVO;
    [SerializeField] private GameObject _greatJobVO;
    private void Start()
    {
        _countdownText.text = "";
    }

    private IEnumerator GameStartRoutine()
    {

        while (_gameIsActive == true)
        {
            _preReadySFX.SetActive(true);
            yield return new WaitForSeconds(_one);
            _countdownText.text = "Ready?";
            _readyVO.SetActive(true);
            yield return new WaitForSeconds(_two);
            _countdownText.text = "3";
            _countdownSFX.SetActive(true);
            yield return new WaitForSeconds(_one);
            _countdownText.text = "2";
            yield return new WaitForSeconds(_one);
            _countdownText.text = "1";
            yield return new WaitForSeconds(_one);
            _countdownText.text = "Start!";
            _startVO.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            _activePowerupGroup = Instantiate(_powerupGroup); // Instantiate Table Top Powerups
            _countdownText.text = "";
            _countdownSFX.SetActive(false);
            _preReadySFX.SetActive(false);
            _readyVO.SetActive(false);
            _startVO.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            ////Starter Set
            _targetGroupScatter.SetActive(true);
            yield return new WaitForSeconds(_nine);
            _targetGroupScatter.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Four Corners
            _targetGroupStill01.SetActive(true);
            yield return new WaitForSeconds(_three);
            _targetGroupStill02.SetActive(true);
            yield return new WaitForSeconds(_five);
            _targetGroupStill01.SetActive(false);
            _targetGroupStill03.SetActive(true);
            yield return new WaitForSeconds(_five);
            _targetGroupStill02.SetActive(false);
            _targetGroupStill04.SetActive(true);
            yield return new WaitForSeconds(_five);
            _targetGroupStill03.SetActive(false);
            yield return new WaitForSeconds(_five);
            _targetGroupStill04.SetActive(false);
            yield return new WaitForSeconds(_three);

            //W Shape Break
            _targetGroupW.SetActive(true);
            yield return new WaitForSeconds(_ten);
            _targetGroupW.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Side to Side Movement
            _middleSideToSide.SetActive(true);
            yield return new WaitForSeconds(_two);
            _topSideToSide.SetActive(true);
            yield return new WaitForSeconds(_two);
            _bottomSideToSide.SetActive(true);
            yield return new WaitForSeconds(_five);
            _middleSideToSide.SetActive(false);
            yield return new WaitForSeconds(_five);
            _topSideToSide.SetActive(false);
            yield return new WaitForSeconds(_five);
            _bottomSideToSide.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Three Stacks of Three
            _targetGroup3Stacks.SetActive(true);
            yield return new WaitForSeconds(_eight);
            _targetGroup3Stacks.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Pyramid Stack Up
            _targetGroupPyramid.SetActive(true);
            yield return new WaitForSeconds(_nine);
            _targetGroupPyramid.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Diagonal Target Groups
            _targetGroupDownLeft01.SetActive(true);
            yield return new WaitForSeconds(_six);
            _targetGroupDownRight01.SetActive(true);
            yield return new WaitForSeconds(_two);
            _targetGroupDownLeft01.SetActive(false);
            _targetGroupDownRight02.SetActive(true);
            yield return new WaitForSeconds(_six);
            _targetGroupDownRight01.SetActive(false);
            _targetGroupDownRight03.SetActive(true);
            yield return new WaitForSeconds(_two);
            _targetGroupDownRight02.SetActive(false);
            yield return new WaitForSeconds(_four);
            _targetGroupDownRight03.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Target Left Movement
            _bottomOneWayLeft.SetActive(true);
            yield return new WaitForSeconds(_one);
            _middleOneWayLeft.SetActive(true);
            yield return new WaitForSeconds(_one);
            _topOneWayLeft.SetActive(true);
            yield return new WaitForSeconds(_one);
            _bottomOneWayLeft.SetActive(false);
            yield return new WaitForSeconds(_one);
            _middleOneWayLeft.SetActive(false);
            yield return new WaitForSeconds(_one);
            _topOneWayLeft.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Diamond Shapes
            _targetGroupDiamondRight.SetActive(true);
            yield return new WaitForSeconds(_four);
            _targetGroupDiamondLeft.SetActive(true);
            yield return new WaitForSeconds(_one);
            _targetGroupDiamondRight.SetActive(false);
            yield return new WaitForSeconds(_four);
            _targetGroupDiamondLeft.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Target Right Movement
            _topOneWayRight.SetActive(true);
            yield return new WaitForSeconds(_one);
            _middleOneWayRight.SetActive(true);
            yield return new WaitForSeconds(_one);
            _bottomOneWayRight.SetActive(true);
            yield return new WaitForSeconds(_one);
            _topOneWayRight.SetActive(false);
            yield return new WaitForSeconds(_one);
            _middleOneWayRight.SetActive(false);
            yield return new WaitForSeconds(_one);
            _bottomOneWayRight.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Pyramid Stack Flipped
            _targetGroupPyramidFlip.SetActive(true);
            yield return new WaitForSeconds(_eight);
            _targetGroupPyramidFlip.SetActive(false);

            //Raining Targets
            _rainingTargets.SetActive(true);
            yield return new WaitForSeconds(25f);
            _rainingTargets.SetActive(false);

            //Ending Stats
            _greatJobVO.SetActive(true);
            yield return new WaitForSeconds(_three);
            _greatJobVO.SetActive(false);

            //Game Over
            _gameIsActive = false;
            _activePowerupGroup.SetActive(false);
        }
    }

    public void GameStartSignal()
    {
        if (_gameIsActive == false && _gameCoroutine == null)
        {
            _gameIsActive = true;
            UIManager.Instance.ResetScore();
            _gameCoroutine = StartCoroutine(GameStartRoutine());
            Debug.Log("Game Start was called from the Button Push");
        }
        else
        {
            Debug.Log("Game is active and cannot start again");

        }
    }

    public void EndGame()
    {
        _gameIsActive = false;
        if (_gameCoroutine != null)
        {
            
            _activePowerupGroup.SetActive(false);
            StopCoroutine(_gameCoroutine);
            _gameCoroutine = null;
            _countdownText.text = "";

            GameObject[] remainingTargets = GameObject.FindGameObjectsWithTag("TargetGroup");
            foreach (GameObject target in remainingTargets)
            {
                target.SetActive(false);
            }

        }
        Debug.Log("EndGame was Called on from the Leave Button");
        //Play End Game SFX
    }
}
