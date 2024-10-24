using System.Collections;
using System.Collections.Generic;
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

    private bool _gameIsActive;

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
    private float _twelve = 12f;

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
        //Set Start Timer

        if (_gameIsActive == true)
        {
            yield return new WaitForSeconds(_three);

            //Starter Set
            _targetGroupScatter.SetActive(true);
            yield return new WaitForSeconds(_nine);
            _targetGroupScatter.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Four Corners
            _targetGroupStill01.SetActive(true);
            yield return new WaitForSeconds(_five);
            _targetGroupStill02.SetActive(true);
            yield return new WaitForSeconds(_nine);
            _targetGroupStill01.SetActive(false);
            _targetGroupStill03.SetActive(true);
            yield return new WaitForSeconds(_nine);
            _targetGroupStill02.SetActive(false);
            _targetGroupStill04.SetActive(true);
            yield return new WaitForSeconds(_nine);
            _targetGroupStill03.SetActive(false);
            yield return new WaitForSeconds(_nine);
            _targetGroupStill04.SetActive(false);
            yield return new WaitForSeconds(_three);

            //W Shape Break
            _targetGroupW.SetActive(true);
            yield return new WaitForSeconds(_nine);
            _targetGroupW.SetActive(false);
            yield return new WaitForSeconds(_three);

            //Side to Side Movement
            _middleSideToSide.SetActive(true);
            yield return new WaitForSeconds(_two);
            _topSideToSide.SetActive(true);
            yield return new WaitForSeconds(_two);
            _bottomSideToSide.SetActive(true);
            yield return new WaitForSeconds(_six);
            _middleSideToSide.SetActive(false);
            yield return new WaitForSeconds(_six);
            _topSideToSide.SetActive(false);
            yield return new WaitForSeconds(_six);
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
            _targetGroupDownLeft02.SetActive(true);
            _targetGroupDownRight02.SetActive(false);
            yield return new WaitForSeconds(_six);
            _targetGroupDownRight03.SetActive(false);
            yield return new WaitForSeconds(_three);

            _gameIsActive = false;

            
        }
    }
}
