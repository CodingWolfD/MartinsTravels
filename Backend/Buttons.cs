using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private GameObject mainMenu; // CREATES A NEW GAMEOBJECT REFERENCE FOR THE MAINMENU
    private GameObject controlsMenu; // CREATES A NEW GAMEOBJECT REFERENCE FOR THE CONTROLSMENU

    private void Start()
    {
        mainMenu = GameObject.Find("Main_Menu"); // INSTANTIATES THE MAINMENU TO THE GAMEOBJECT "MAINMENU" FOUND IN THE SCENE
        controlsMenu = GameObject.Find("Controls_Menu"); // INSTANTIATES THE CONTROLSMENU TO THE GAMEOBJECT "CONTROLSMENU" FOUND IN THE SCENE

        controlsMenu.SetActive(false); // DISABLES THE CONTROLSMENU WHEN THE GAME STARTS
    }

    public void startGame() // CREATES A NEW METHOD FOR THE STARTGAME BUTTON
    {
        SceneManager.LoadScene(2); // LOADS THE SECOND LEVEL
    }

    public void exitGame() // CREATES A NEW METHOD FOR THE EXIT GAME BUTTON
    {
        Application.Quit(); // TELLS THE APPLICATION TO QUIT
    }

    public void openControls()
    {
        controlsMenu.SetActive(true); // ENABLES THE CONTROLS MENU GAMEOBJECT
        mainMenu.SetActive(false); // DISABLES THE MAINMENU GAMEOBJECT
    }

    public void returnToMenu()
    {
        controlsMenu.SetActive(false); // DISABLES THE CONTROLS MENU GAMEOBJECT
        mainMenu.SetActive(true); // ENABLES THE MAINMENU GAMEOBJECT
    }
}