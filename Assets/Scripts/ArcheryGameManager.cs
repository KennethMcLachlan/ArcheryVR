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

    
    private void Start()
    {
        _countdownText.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _gameIsActive = true;
            StartCoroutine(GameStartRoutine());
        }
            

        
    }
    private IEnumerator GameStartRoutine()
    {

        while (_gameIsActive == true)
        {
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
            _activePowerupGroup = Instantiate(_powerupGroup); // Instantiate Table Top Powerups
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
            EndGame();
        }
    }

    public void GameStartSignal()
    {
        if (_gameIsActive == false && _gameCoroutine == null)
        {
            UIManager.Instance.ResetScore();

            if (_scoreboard != null)
            {
                _scoreboard.SetActive(false);
            }

            _gameIsActive = true;
            _archeryHostessVO.SetActive(false);
            UIManager.Instance.ResetScore();
            _gameCoroutine = StartCoroutine(GameStartRoutine());
            Debug.Log("Game Start was called from the Button Push");
        }
    }

    public void EndGame()
    {
        _gameIsActive = false;

        if (_activePowerupGroup != null)
        {
            _activePowerupGroup.SetActive(false);
        }

        if (_scoreboard != null)
        {
            _scoreboard.SetActive(true);
        }

        _archeryHostessVO.SetActive(true);
        //StopCoroutine(_gameCoroutine);
        if (_gameCoroutine != null)
        {
            _gameCoroutine = null;
        }
        _countdownText.text = "";

        GameObject[] remainingTargets = GameObject.FindGameObjectsWithTag("TargetGroup");
        foreach (GameObject target in remainingTargets)
        {
            Destroy(target);
        }


        Debug.Log("EndGame was Called on from the Leave Button");
    }


    //private IEnumerator GameStartRoutine()
    //{

    //    while (_gameIsActive == true)
    //    {
    //        _scoreboard.SetActive(false);
    //        _preReadySFX.SetActive(true);
    //        yield return new WaitForSeconds(_one);
    //        _countdownText.text = "Ready?";
    //        _readyVO.SetActive(true);
    //        yield return new WaitForSeconds(_two);
    //        _countdownText.text = "3";
    //        _countdownSFX.SetActive(true);
    //        yield return new WaitForSeconds(_one);
    //        _countdownText.text = "2";
    //        yield return new WaitForSeconds(_one);
    //        _countdownText.text = "1";
    //        yield return new WaitForSeconds(_one);
    //        _countdownText.text = "Start!";
    //        _startVO.SetActive(true);
    //        yield return new WaitForSeconds(1.5f);
    //        _activePowerupGroup = Instantiate(_powerupGroup); // Instantiate Table Top Powerups
    //        _countdownText.text = "";
    //        _countdownSFX.SetActive(false);
    //        _preReadySFX.SetActive(false);
    //        _readyVO.SetActive(false);
    //        _startVO.SetActive(false);
    //        yield return new WaitForSeconds(0.5f);

    //        ////Starter Set
    //        _targetGroupScatter.SetActive(true);
    //        yield return new WaitForSeconds(_nine);
    //        _targetGroupScatter.SetActive(false);
    //        yield return new WaitForSeconds(_three);

    //        //Four Corners
    //        _targetGroupStill01.SetActive(true);
    //        yield return new WaitForSeconds(_three);
    //        _targetGroupStill02.SetActive(true);
    //        yield return new WaitForSeconds(_five);
    //        _targetGroupStill01.SetActive(false);
    //        _targetGroupStill03.SetActive(true);
    //        yield return new WaitForSeconds(_five);
    //        _targetGroupStill02.SetActive(false);
    //        _targetGroupStill04.SetActive(true);
    //        yield return new WaitForSeconds(_five);
    //        _targetGroupStill03.SetActive(false);
    //        yield return new WaitForSeconds(_five);
    //        _targetGroupStill04.SetActive(false);

    //        //W Shape Break
    //        _targetGroupW.SetActive(true);
    //        yield return new WaitForSeconds(_ten);
    //        _targetGroupW.SetActive(false);
    //        yield return new WaitForSeconds(_three);

    //        //Side to Side Movement
    //        _middleSideToSide.SetActive(true);
    //        yield return new WaitForSeconds(_two);
    //        _topSideToSide.SetActive(true);
    //        yield return new WaitForSeconds(_two);
    //        _bottomSideToSide.SetActive(true);
    //        yield return new WaitForSeconds(_five);
    //        _middleSideToSide.SetActive(false);
    //        yield return new WaitForSeconds(_five);
    //        _topSideToSide.SetActive(false);
    //        yield return new WaitForSeconds(_five);
    //        _bottomSideToSide.SetActive(false);
    //        yield return new WaitForSeconds(_three);

    //        //Three Stacks of Three
    //        _targetGroup3Stacks.SetActive(true);
    //        yield return new WaitForSeconds(_eight);
    //        _targetGroup3Stacks.SetActive(false);
    //        yield return new WaitForSeconds(_three);

    //        //Pyramid Stack Up
    //        _targetGroupPyramid.SetActive(true);
    //        yield return new WaitForSeconds(_nine);
    //        _targetGroupPyramid.SetActive(false);
    //        yield return new WaitForSeconds(_three);

    //        //Diagonal Target Groups
    //        _targetGroupDownLeft01.SetActive(true);
    //        yield return new WaitForSeconds(_six);
    //        _targetGroupDownRight01.SetActive(true);
    //        yield return new WaitForSeconds(_two);
    //        _targetGroupDownLeft01.SetActive(false);
    //        _targetGroupDownRight02.SetActive(true);
    //        yield return new WaitForSeconds(_six);
    //        _targetGroupDownRight01.SetActive(false);
    //        _targetGroupDownRight03.SetActive(true);
    //        yield return new WaitForSeconds(_two);
    //        _targetGroupDownRight02.SetActive(false);
    //        yield return new WaitForSeconds(_four);
    //        _targetGroupDownRight03.SetActive(false);
    //        yield return new WaitForSeconds(_three);

    //        //Target Left Movement
    //        _bottomOneWayLeft.SetActive(true);
    //        yield return new WaitForSeconds(_one);
    //        _middleOneWayLeft.SetActive(true);
    //        yield return new WaitForSeconds(_one);
    //        _topOneWayLeft.SetActive(true);
    //        yield return new WaitForSeconds(_one);
    //        _bottomOneWayLeft.SetActive(false);
    //        yield return new WaitForSeconds(_one);
    //        _middleOneWayLeft.SetActive(false);
    //        yield return new WaitForSeconds(_one);
    //        _topOneWayLeft.SetActive(false);
    //        yield return new WaitForSeconds(_three);

    //        //Diamond Shapes
    //        _targetGroupDiamondRight.SetActive(true);
    //        yield return new WaitForSeconds(_four);
    //        _targetGroupDiamondLeft.SetActive(true);
    //        yield return new WaitForSeconds(_one);
    //        _targetGroupDiamondRight.SetActive(false);
    //        yield return new WaitForSeconds(_four);
    //        _targetGroupDiamondLeft.SetActive(false);
    //        yield return new WaitForSeconds(_three);

    //        //Target Right Movement
    //        _topOneWayRight.SetActive(true);
    //        yield return new WaitForSeconds(_one);
    //        _middleOneWayRight.SetActive(true);
    //        yield return new WaitForSeconds(_one);
    //        _bottomOneWayRight.SetActive(true);
    //        yield return new WaitForSeconds(_one);
    //        _topOneWayRight.SetActive(false);
    //        yield return new WaitForSeconds(_one);
    //        _middleOneWayRight.SetActive(false);
    //        yield return new WaitForSeconds(_one);
    //        _bottomOneWayRight.SetActive(false);
    //        yield return new WaitForSeconds(_three);

    //        //Pyramid Stack Flipped
    //        _targetGroupPyramidFlip.SetActive(true);
    //        yield return new WaitForSeconds(_eight);
    //        _targetGroupPyramidFlip.SetActive(false);

    //        //Raining Targets
    //        _rainingTargets.SetActive(true);
    //        yield return new WaitForSeconds(25f);
    //        _rainingTargets.SetActive(false);

    //        //Ending Stats
    //        _greatJobVO.SetActive(true);
    //        _scoreboard.SetActive(true);
    //        UIManager.Instance.DisplayScore();
    //        yield return new WaitForSeconds(_three);
    //        _greatJobVO.SetActive(false);

    //        //Game Over
    //        _activePowerupGroup.SetActive(false);
    //        EndGame();
    //    }
    //}

}
