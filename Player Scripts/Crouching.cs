using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{
    private CharacterController cc; // REFERENCE TO THE CHARACTER CONTROLLER GAMEOBJECT
    private bool crouched; // THIS BOOL IS USED TO TELL IF THE PLAYER IS CROUCHING
    private GameObject crouchInd; // REFERENCE FOR THE CROUCH INDICATOR
    private int fadeSpeed; // USED FOR MAKING THE CROUCH INDICATOR TRANSPARENT

    private void Awake ()
    {
        fadeSpeed = 1;
        crouched = false; // SETS CROUCHED TO FALSE
        crouchInd = GameObject.Find("CrouchInd"); // GETS THE CROUCH INDICATOR FROM THE CANVAS
        cc = GameObject.Find("FPSController").GetComponent<CharacterController>(); // GETS THE CHARACTER CONTROLLER SCRIPT FROM THE FPS CONTROLLER

        crouchInd.SetActive(false);
    }

    private void Update ()
    {
		if(Input.GetKeyDown(KeyCode.LeftControl) && !crouched) // IF THE PLAYER PRESSES THE LEFT CONTROL AND THE PLAYER ISNT CROUCHED
        {
            crouch(); // CALLS THE CROUCH METHOD
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && crouched) // IF THE PLAYER PRESSES THE LEFT CONTROL AND THE PLAYER IS CROUCHED
        {
            uncrouch(); // CALLS THE UNCROUCH METHOD
        }
    }

    private void crouch()
    {
        crouchInd.SetActive(true);
        cc.height = 1; // SETS THE PLAYER HEIGHT TO 1
        crouched = true; // SETS THE PlAYERS STATE TO CROUCHED
    }

    private void uncrouch()
    {
        crouchInd.SetActive(false);
        cc.height = 2; // SETS THE PLAYER HEIGHT BACK TO 2
        crouched = false; // SETS THE PLAYERS STATE TO UNCROUCHED
    }
}