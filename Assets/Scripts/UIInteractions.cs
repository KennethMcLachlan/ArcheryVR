using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInteractions : MonoBehaviour
{
    public MenuController _pauseMenu;

    private void Start()
    {
        _pauseMenu = GetComponent<MenuController>();
    }

    public void ResumeGame()
    {
        _pauseMenu.ResumeGameFromButton();
    }
    private void QuitGame()
    {
        Application.Quit();
    }


}
