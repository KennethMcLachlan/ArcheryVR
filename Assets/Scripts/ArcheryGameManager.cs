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
    [SerializeField] private GameObject _rainingTargets;
    [SerializeField] private GameObject _topSideToSide;
    [SerializeField] private GameObject _middleSideToSide;
    [SerializeField] private GameObject _bottomSideToSide;
    [SerializeField] private GameObject _topOneWayRight;
    [SerializeField] private GameObject _middleOneWayRight;
    [SerializeField] private GameObject _bottomOneWayRight;
    [SerializeField] private GameObject _topOneWayLeft;
    [SerializeField] private GameObject _middleOneWayLeft;
    [SerializeField] private GameObject _bottomOneWayLeft;

    //Parent Objects to spawn Targets in
    [SerializeField] private Transform _targetArea;
    [SerializeField] private Transform _groupScatterSpawn;
    [SerializeField] private Transform _groupStill01Spawn;
    [SerializeField] private Transform _groupStill02Spawn;
    [SerializeField] private Transform _groupStill03Spawn;
    [SerializeField] private Transform _groupStill04Spawn;

    [SerializeField] private Transform _downLeft01Spawn;
    [SerializeField] private Transform _downRight01Spawn;
    [SerializeField] private Transform _downRight02Spawn;
    [SerializeField] private Transform _downRight03Spawn;

    [SerializeField] private Transform _diamondLeftSpawn;
    [SerializeField] private Transform _diamondRightSpawn;


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
    private bool _gameIsFinished;

    //Spawn/Despawn Powerups
    [SerializeField] private GameObject _powerupGroup;
    private GameObject _activePowerupGroup;

    //SFX
    [SerializeField] private GameObject _countdownSFX;
    [SerializeField] private GameObject _preReadySFX;
    [SerializeField] private GameObject _readyVO;
    [SerializeField] private GameObject _startVO;
    [SerializeField] private GameObject _greatJobVO;

    [SerializeField] private GameObject _archeryHostessVO;
    [SerializeField] private GameObject _scoreboard;

    [SerializeField] private GameObject _playButton;

    private void Start()
    {
        _countdownText.text = "";
        _gameIsActive = false;
    }

    private IEnumerator GameStartRoutine()
    {
        _gameIsActive = true;

        while (_gameIsActive == true)
        {
            _archeryHostessVO.SetActive(false);
            UIManager.Instance.ResetScore();

            _scoreboard.SetActive(false);
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
            _activePowerupGroup = Instantiate(_powerupGroup);
            _countdownText.text = "";
            _countdownSFX.SetActive(false);
            _preReadySFX.SetActive(false);
            _readyVO.SetActive(false);
            _startVO.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            ////Starter Set
            GameObject targetGroupScatter = Instantiate(_targetGroupScatter, _groupScatterSpawn.position, _groupScatterSpawn.rotation);
            yield return new WaitForSeconds(_nine);
            Destroy(targetGroupScatter);
            yield return new WaitForSeconds(_three);

            //Four Corners
            GameObject targetGroupStill01 = Instantiate(_targetGroupStill01,  _groupStill01Spawn.position, _groupStill01Spawn.rotation);
            yield return new WaitForSeconds(_three);
            GameObject targetGroupStill02 = Instantiate(_targetGroupStill02, _groupStill02Spawn.position, _groupStill02Spawn.rotation);
            yield return new WaitForSeconds(_five);
            Destroy(targetGroupStill01);
            GameObject targetGroupStill03 = Instantiate(_targetGroupStill03, _groupStill03Spawn.position, _groupStill03Spawn.rotation);
            yield return new WaitForSeconds(_five);
            Destroy(targetGroupStill02);
            GameObject targetGroupStill04 = Instantiate(_targetGroupStill04, _groupStill04Spawn.position, _groupStill04Spawn.rotation);
            yield return new WaitForSeconds(_five);
            Destroy(targetGroupStill03);
            yield return new WaitForSeconds(_three);
            Destroy(targetGroupStill04);

            //W Shape Break
            GameObject targetGroupW = Instantiate(_targetGroupW, _targetArea.position, _targetArea.rotation);
            yield return new WaitForSeconds(_ten);
            Destroy(targetGroupW);
            yield return new WaitForSeconds(_three);

            //Side to Side Movement
            GameObject middleSideToSide = Instantiate(_middleSideToSide, _targetArea.position, _targetArea.rotation);
            middleSideToSide.transform.SetParent(_targetArea);
            middleSideToSide.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(_two);
            GameObject topSideToSide = Instantiate(_topSideToSide, _targetArea.position, _targetArea.rotation);
            topSideToSide.transform.SetParent(_targetArea);
            topSideToSide.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(_two);
            GameObject bottomSideToSide = Instantiate(_bottomSideToSide, _targetArea.position, _targetArea.rotation);
            bottomSideToSide.transform.SetParent(_targetArea);
            bottomSideToSide.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(_five);
            Destroy(middleSideToSide);
            yield return new WaitForSeconds(_five);
            Destroy(topSideToSide);
            yield return new WaitForSeconds(_five);
            Destroy(bottomSideToSide);
            yield return new WaitForSeconds(_three);

            //Three Stacks of Three
            GameObject targetGroup3Stacks = Instantiate(_targetGroup3Stacks, _targetArea.position, _targetArea.rotation);
            yield return new WaitForSeconds(_eight);
            Destroy(targetGroup3Stacks);
            yield return new WaitForSeconds(_three);

            //Pyramid Stack Up
            GameObject targetGroupPyramid = Instantiate(_targetGroupPyramid, _targetArea.position, _targetArea.rotation);
            yield return new WaitForSeconds(_nine);
            Destroy(targetGroupPyramid);
            yield return new WaitForSeconds(_three);

            //Diagonal Target Groups
            GameObject targetGroupDownLeft01 = Instantiate(_targetGroupDownLeft01, _downLeft01Spawn.position, _downLeft01Spawn.rotation);
            yield return new WaitForSeconds(_six);
            GameObject targetGroupDownRight01 = Instantiate(_targetGroupDownRight01, _downRight01Spawn.position, _downRight01Spawn.rotation);
            yield return new WaitForSeconds(_two);
            Destroy(targetGroupDownLeft01);
            GameObject targetGroupDownRight02 = Instantiate(_targetGroupDownRight02, _downRight02Spawn.position, _downRight02Spawn.rotation);
            yield return new WaitForSeconds(_six);
            Destroy(targetGroupDownRight01);
            GameObject targetGroupDownRight03 = Instantiate(_targetGroupDownRight03, _downRight03Spawn.position, _downRight03Spawn.rotation);
            yield return new WaitForSeconds(_two);
            Destroy(targetGroupDownRight02);
            yield return new WaitForSeconds(_four);
            Destroy(targetGroupDownRight03);
            yield return new WaitForSeconds(_three);

            //Target Left Movement
            GameObject bottomOneWayLeft = Instantiate(_bottomOneWayLeft, _targetArea.position, _targetArea.rotation);
            bottomOneWayLeft.transform.SetParent(_targetArea);
            bottomOneWayLeft.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(_one);
            GameObject middleOneWayLeft = Instantiate(_middleOneWayLeft, _targetArea.position, _targetArea.rotation);
            middleOneWayLeft.transform.SetParent(_targetArea);
            middleOneWayLeft.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(_one);
            GameObject topOneWayLeft = Instantiate(_topOneWayLeft, _targetArea.position, _targetArea.rotation);
            topOneWayLeft.transform.SetParent(_targetArea);
            topOneWayLeft.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(_one);
            Destroy(bottomOneWayLeft);
            yield return new WaitForSeconds(_one);
            Destroy(middleOneWayLeft);
            yield return new WaitForSeconds(_one);
            Destroy(topOneWayLeft);
            yield return new WaitForSeconds(_three);

            //Diamond Shapes
            GameObject targetGroupDiamondRight = Instantiate(_targetGroupDiamondRight, _diamondRightSpawn.position, _diamondRightSpawn.rotation);
            yield return new WaitForSeconds(_four);
            GameObject targetGroupDiamondLeft = Instantiate(_targetGroupDiamondLeft, _diamondLeftSpawn.position, _diamondLeftSpawn.rotation);
            yield return new WaitForSeconds(_one);
            Destroy(targetGroupDiamondRight);
            yield return new WaitForSeconds(_four);
            Destroy(targetGroupDiamondLeft);
            yield return new WaitForSeconds(_three);

            //Target Right Movement
            GameObject topOneWayRight = Instantiate(_topOneWayRight, _targetArea.position, _targetArea.rotation);
            topOneWayRight.transform.SetParent(_targetArea);
            topOneWayRight.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(_one);
            GameObject middleOneWayRight = Instantiate(_middleOneWayRight, _targetArea.position, _targetArea.rotation);
            middleOneWayRight.transform.SetParent(_targetArea);
            middleOneWayRight.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(_one);
            GameObject bottomOneWayRight = Instantiate(_bottomOneWayRight, _targetArea.position, _targetArea.rotation);
            bottomOneWayRight.transform.SetParent(_targetArea);
            bottomOneWayRight.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(_one);
            Destroy(topOneWayRight);
            yield return new WaitForSeconds(_one);
            Destroy(middleOneWayRight);
            yield return new WaitForSeconds(_one);
            Destroy(bottomOneWayRight);
            yield return new WaitForSeconds(_three);

            //Pyramid Stack Flipped
            GameObject targetGroupPyramidFlip = Instantiate(_targetGroupPyramidFlip, _targetArea.position, _targetArea.rotation);
            yield return new WaitForSeconds(_eight);
            Destroy(targetGroupPyramidFlip);

            //Raining Targets
            GameObject rainingTargets = Instantiate(_rainingTargets, _targetArea.position, _targetArea.rotation);
            rainingTargets.transform.SetParent(_targetArea);
            rainingTargets.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(25f);
            Destroy(rainingTargets);

            //Ending Stats
            _greatJobVO.SetActive(true);
            _scoreboard.SetActive(true);
            UIManager.Instance.DisplayScore();
            yield return new WaitForSeconds(_three);
            _greatJobVO.SetActive(false);

            //Game Over
            _activePowerupGroup.SetActive(false);
            _archeryHostessVO.SetActive(true);
            _playButton.SetActive(true);
            _gameIsActive = false;
        }
    }

    public void GameStartSignal()
    {
        _playButton.SetActive(false);
        _gameCoroutine = StartCoroutine(GameStartRoutine());
        Debug.Log("Game Start was called from the Button Push");

    }

}
