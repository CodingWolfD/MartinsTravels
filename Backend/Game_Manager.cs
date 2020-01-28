using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    private InteractiveObjects interObj; // REFERENCE TO THE INTERACTIVE OBJECTS SCRIPT
    private SubtitlesandObjectives intrRPS; // REFERENCE TO THE INTERACTIVE RESPONSE SCRIPT
    private Camera cam; // REFERNCE TO THE CAMERA
    private Text interactionText; // REFERENCE TO THE TEXT OBJECT FOR INTERACTIVE OBJECTS
    public LayerMask layerMask; // SETS THE LAYERMASK FOR THE OBJECTS DETECTED BY RAYCAST
    private float rayLength = 3; // SETS THE LENGTH OF THE RAYCAST
    private FirstPersonController fpsc; // REFERENCE TO THE FPS CONTROLLER USED FOR THE PLAYER
    private GameObject pauseMenu; // REFERENCE TO THE PAUSE MENU GAMEOBJECT

    private GameObject HUD; // REFERENCE TO THE HEALTHBAR IN GAME

    private GameObject endGameMenu; // REFERENCE TO THE ENDGAME MENU

    private void Awake()
    {
        setup(); // SETUP METHOD
    }

    private void setup()
    {
        fpsc = GameObject.Find("FPSController").GetComponent<FirstPersonController>(); // GETS THE FPSCONTROLLER SCRIPT ATTACHED TO THE PLAYER
        pauseMenu = GameObject.Find("PauseMenu"); // GETS THE PAUSE MENU FROM THE CANVAS
        HUD = GameObject.Find("HUD"); // GETS THE OBJECTIVE FROM THE CANVAS
        intrRPS = GameObject.Find("Game_Manager").GetComponent<SubtitlesandObjectives>(); // GETS THE GAME_MANAGER GAMEOBJECT AND ACCESSES THE INTERACTIVE RESPONSE SCRIPT
        interObj = GetComponent<InteractiveObjects>(); // REFERENCES THE GAME_MANAGER COMPONENT AND ACCESSES THE INTERACTIVE OBJECT SCRIPT
        cam = GameObject.Find("Player").GetComponent<Camera>(); // FINDS THE MAIN CAMERA THE PLAYER USES
        interactionText = GameObject.Find("InteractionText").GetComponent<Text>(); // FINDS THE INTERACTIVE TEXT FOR INTERACTABLE OBJECTS

        endGameMenu = GameObject.Find("EndGame_Menu"); // FINDS THE ENDGAME MENU IN THE SCENE

        endGameMenu.SetActive(false); // SETS THE ENDGAME MENU TO DEACTIVE WHEN THE GAME STARTS
        pauseMenu.SetActive(false); // DISABLES THE PAUSE MENU ON GAME START
        intrRPS.setObj("New Objective: \nPick-up the flashlight on the drawer next to the bed"); // TELLS THE PLAYER TO PICKUP THE FLASHLIGHT 
        intrRPS.clearObjective(); // CLEARS THE CURRENT OBJECTIVE AFTER 3 SECONDS

        //Cursor.lockState = CursorLockMode.Locked; // SETS THE CURSOR LOCKSTATE TO LOCKED WHEN THE GAME STARTS
        Cursor.visible = true; // SETS THE CURSORS VISIBILITY STATE TO INVISIBLE
    }

    public void enableRayCast() // ENABLES THE RAYCAST FOR THE INTERACTIVE OBJECTS
    {
        RaycastHit hit; // DEFINES THE RAYCAST COLLIDER

        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // SETS THE RAYCAST TO BE IN THE MIDDLE OF THE SCREEN
        Debug.DrawRay(new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z), ray.direction, Color.black); // DRAWS THE RAY IN THE EDITOR FOR DEBUGGING

        if (Physics.Raycast(ray, out hit, rayLength, layerMask)) // IF THE RAYCAST HITS AN OBJECT
        {
            if (hit.collider != null) // IF THE RAYCAST COLLIDER IS NOT NULL
            {
                hit.collider.enabled = true; // ENABLE THE RAYCAST COLLIDER

                if (hit.collider.tag == "Interactable") // IF THE COLLIDER HITS AN INTERACTABLE OBJECT
                {
                    interactionText.text = "      " + hit.collider.name + "\n Press [E] to use"; // SET THE INTERACTION TEXT TO TELL THE PLAYER WHICH BUTTON TO PRESS TO INTERACT WITH THE OBJECT

                    if (Input.GetKeyDown(KeyCode.E)) // IF THE PLAYER PRESSES THE CORRECT USE KEY
                    {
                        interObj.checkObject(hit.collider.name, hit); // CALL THE CHECKOBJECT METHOD WITH THE PARAMETER OF THE RAYCASTS HIT OBJECT
                    }
                }
                else if (hit.collider.tag == "Vehicle") // IF THE COLLIDER HITS AN INTERACTABLE OBJECT
                {
                    interactionText.text = "      " + hit.collider.name + "\n Press [E] to get in"; // SET THE INTERACTION TEXT TO TELL THE PLAYER WHICH BUTTON TO PRESS TO INTERACT WITH THE OBJECT

                    if (Input.GetKeyDown(KeyCode.E)) // IF THE PLAYER PRESSES THE CORRECT USE KEY
                    {
                        interObj.checkObject(hit.collider.name, hit); // CALL THE CHECKOBJECT METHOD WITH THE PARAMETER OF THE RAYCASTS HIT OBJECT
                    }
                }
                else
                {
                    interactionText.text = ""; // SETS THE INTERACTION TEXT TO BLANK
                }
            }
        }
    }

    public void playAnimation(GameObject gameObject, string animationName) // plays the animation give in the parameters using the animationName
    {
        gameObject.GetComponent<Animator>().Play(animationName); // Gets the Gameobject name and accesses the Animator component and plays the animationName
    }

    public void deactivateObject(GameObject gObject) // Deactivates the GameObject based on the parameter passed in 
    {
        gObject.SetActive(false); // Deactivates the current gameobject using the GameObject passed in through the parameter
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !fpsc.getPaused()) // CHECKS IF THE PLAYER PRESSES ESCAPE AND THE GAME ISNT PAUSED
        {
            pauseGame(); // CALLS THE PAUSEGAME METHOD
        }
    }

    private void pauseGame()
    {
        fpsc.setPaused(true); // SET THE GAMES STATE TO PAUSED
        pauseMenu.SetActive(true); // ENABLES THE PAUSE MENU
        Time.timeScale = 0; // SET THE GAMES TIME SCALE TO 0
        HUD.SetActive(false); // DISABLES THE HEALTHBAR IF THE GAME IS PAUSED

        Cursor.lockState = CursorLockMode.None; // SETS THE CURSORS LOCKSTATE TO NONE
        Cursor.visible = true; // SETS THE CURSOR TO VISIBLE
    }

    public void unPauseGame()
    {
        HUD.SetActive(true); // SETS THE HUD GAMEOBJECT TO ACTIVE
        fpsc.setPaused(false); // SET THE GAME STATE TO UNPAUSED
        pauseMenu.SetActive(false); // DISABLES THE PAUSE MENU
        Time.timeScale = 1; // SETS THE GAMES TIME SCALE TO 1

        //Cursor.lockState = CursorLockMode.Locked; // SETS THE CURSORS LOCKSTATE TO LOCKED
        //Cursor.visible = false; // SETS THE CURSOR TO INVISIBLE
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0); // LOADS THE SCENE 0 
        Time.timeScale = 1; // SETS THE GAMES TIMESCALE BACK TO 1
    }

    public void resetLevel()
    {
        SceneManager.LoadScene(1); // LOADS THE SCENE 1
        Time.timeScale = 1; // SETS THE TIMESCALE BACK TO 1
    }

    public void endGame()
    {
        Time.timeScale = 0; // SETS THE TIMESCALE TO 0
        endGameMenu.SetActive(true); // SETS THE GAMEOVER SCREEN TO ACTIVE

        Cursor.visible = true; // SETS THE CURSOR TO VISIBLE
        Cursor.lockState = CursorLockMode.None; // SETS THE CURSORS LOCKMODE TO NONE
    }
}
// WRITTEN BY DANIEL MARTIN