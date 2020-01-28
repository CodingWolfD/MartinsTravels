using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjects : MonoBehaviour
{
    private SubtitlesandObjectives intrRPS; // REFERENCE TO THE INTERACTIVE RESPONSE SCRIPT
    private Game_Manager gm; // REFERENCE TO THE GAME MANAGER
    private GameObject keypad; // REFERENCE TO THE KEYPAD IN GAME
    private GameObject hand; // REFERENCE TO THE HAND GAMEOBJECT
    private ParticleSystem sparks; // REFERENCE TO THE PARTICLE SYSTEM FOR THE KEYPAD
    private ParticleSystem smoke; // REFERENCE TO THE PARTICLE SYSTEM FOR SMOKE 
    private GameObject door; // REFERENCE TO THE DOOR GAMEOBJECT IN GAME
    private AudioSource buzz; // REFERENCE TO THE BUZZING AUDIO FILE ON THE KEYPAD
    private Flashlight flash; // REFERENCE TO THE FLASHLIGHT SCRIPT
    private GameObject hiddenDoor; // REFERENCE FOR HIDDEN DOOR
    private int batteryPower; // REFERENCE TO THE AMOUNT OF POWER ADDED WHEN EACH BATTERY IS COLLECTED
    private JohnsDialogue jd; // CREATES A REFERENCE TO JOHNS DIALOGUE SCRIPT

    private void Awake()
    {
        setup(); // GETS ALL THE GAMEOBJECTS NEEDED IN GAME
    }

    private void setup()
    {
        intrRPS = GameObject.Find("Game_Manager").GetComponent<SubtitlesandObjectives>(); // GETS THE GAME_MANAGER GAMEOBJECT AND ACCESSES THE INTERACTIVE RESPONSE SCRIPT
        door = GameObject.Find("Door"); // GETS THE BROKEN DOOR GAMEOBJECT IN GAME
        smoke = GameObject.Find("Smoke").GetComponent<ParticleSystem>(); // GETS THE SMOKE PARTICLE SYSTEM FOR THE KEYPAD
        sparks = GameObject.Find("Sparks").GetComponent<ParticleSystem>(); // GETS THE KEYPADS SPARK PARTICLE SYSTEM FOR WHEN THE KEYPAD IS BROKEN
        gm = GameObject.Find("Game_Manager").GetComponent<Game_Manager>(); // GETS THE GAME MANAGER FOT THE BACKGROUND PROCESSESS OF THE GAME
        keypad = GameObject.Find("Keypad"); // FINDS THE KEYPAD GAMEOBJECT IN GAME
        flash = GameObject.Find("Flashlight").GetComponent<Flashlight>(); // FINDS THE KEYPAD GAMEOBJECT IN GAME
        hand = GameObject.Find("Hand"); // FINDS THE KEYPAD GAMEOBJECT IN GAME
        buzz = keypad.GetComponent<AudioSource>(); // GETS THE KEYPADS AUDIOSOURCE FOR BUZZING SOUND
        hiddenDoor = GameObject.Find("Hidden_Door"); // FINDS THE HIDDEN DOOR GAMEOBJECT IN GAME
        jd = GameObject.Find("John").GetComponent<JohnsDialogue>(); // FINDS JOHN IN THE GAME AND CREATES A REFERENCE TO HIS DIALOGUE SCRIPT

        buzz.Stop(); // STOPS THE BUZZING SOUND FROM PLAYING WHEN THE GAME IS STARTED
        smoke.Stop(); // STOPS THE SMOKE PARTICLES FROM PLAYING UNTIL KEYPAD IS BROKEN
        sparks.Stop(); // STOPS THE SPARKS PARTICLES PLAYING UNTIL PLAYER ACTIVATES KEYPAD

        intrRPS.setSub("Hello? Is anybody there? Where am i?"); // SETS THE FIRST SUBTITLE TO "UGH MY HEAD, WHERE AM I?"
        intrRPS.clearSub(); // STARTS THE COROUTINES AND THE CLEARS THE SUBTITLE

        batteryPower = 25; // SETS EACH BATTERYS POWER TO 10 
    }

    private void Update()
    { 
        gm.enableRayCast(); // ENABLES THE RAYCAST FOR THE PLAYER 
    }

    public void checkObject(string objectName, RaycastHit hit)
    {   
        switch (objectName) // SWITCH STATEMENT ON THE OBJECTS NAME
        {      
              case "Keypad": // IF PLAYER PRESSES THE EQUIP BUTTON ON THE KEYPAD
              {
                   gm.playAnimation(keypad, "Falling"); // PLAYS THE FALLING ANIMATION FOR THE KEYPAD AFTER PLAYER PRESSES E
                   gm.playAnimation(door, "Opening"); // PLAYS THE BROKEN DOOR ANIMATION
                   buzz.Play(); // PLAYS THE BUZZING SOUND FOR THE BROKEN KEYPAD
                   sparks.Play(); // PLAYS THE SPARKS PARTICLE EFFECTS WHEN THE KEYPAD IS BROKEN
                   smoke.Play(); // PLAYS THE SMOKE PARTICLE EFFECT WHEN THE KEYPAD IS BROKEN
                   intrRPS.setSub("Guess i'll need to find another way out"); // DISPLAYS SUBTITLE TO GIVE INFORMATION TO THE PLAYER
                   intrRPS.clearSub(); // CLEARS THE SUBTITLE AFTER 3 SECONDS
                   keypad.GetComponent<BoxCollider>().enabled = false; // DISABLES THE BOX COLLIDER FOR THE KEYPAD SO IT CANT BE REACTIVATED
                   hiddenDoor.SetActive(false); // DISABLES THE HIDDEN DOOR FOR THE VENT

                   intrRPS.setObj("New Objective: \nFind another way out"); // TELLS THE PLAYER TO FIND ANOTHER WAY OUT WHEN THE KEYPAD IS BROKEN
                   intrRPS.clearObjective(); // CLEARS THE OBJECTIVE AFTER 3 SECONDS
              }
                 break;
              case "Flashlight": // IF PLAYER PRESSES THE EQUIP BUTTON ON THE FLASHLIGHT
              {
                   intrRPS.setSub("A Flashlight, could be useful"); // DISPLAYS SUBTITLE TO GIVE INFORMATION TO THE PLAYER
                   intrRPS.clearSub(); // CLEARS THE SUBTITLE AFTER 3 SECONDS
                   flash.transform.SetParent(hand.transform); // SETS THE FLASHLIGHT PARENT TO THE HAND
                   flash.transform.rotation = hand.transform.rotation; // ADJUSTS THE FLASHLIGHTS ROTATION BASED ON X, Y AND Z CO-ORDINATES
                   flash.transform.position = new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z); // SETS THE FLASHLIGHTS NEW POSITION TO THE PLAYERS HAND
                   flash.setFLash(true); // SETS THE FLASH EQUIPED STATE TO TRUE
                   intrRPS.setObj("Objective Completed: \nPicked up the flashlight"); // TELLS THE PLAYER THEY HAVE COMPLETED TGE CURRENT OBJECTIVE
                   intrRPS.clearObjective(); // CLEARS THE OBJECTIVE AFTER 3 SECONDS
              }
                break;
             case "Battery": // IF THE PLAYER PICKS UP BATTERIES 
             {
                 flash.setPower(batteryPower); // ADDS THE BATTERY POWER TO THE FLASHLIGHTS POWER
                 Destroy(hit.collider.gameObject); // DESTROYS THE BATTERY GAMEOBJECT ONCE POWER HAS BEEN ADDED TO THE FLASHLIGHTS CURRENT POWER
             }
                break;
             case "The Employer": // IF THE PLAYER PRESSES [E] ON THE NPC CHARACTER
             { 
                intrRPS.setObj("New Objective: \nCollect the file from this building"); // TELLS THE PLAYER TO FIBD THE FILE HIDDEN IN THE CURRENT BUILDING
                intrRPS.clearObjective(); // CLEARS THE OBJECTIVE AFTER 3 SECONDS
             }
               break;
            case "File":
            {
               Destroy(hit.collider.gameObject); // THIS DESTOYS THE FILE ONCE THE PLAYER HAS PRESSED E ON THE FILE OBJECT
               intrRPS.setObj("New Objective: \nFind and speak to John"); // TELLS THE PLAYER TO FIBD THE FILE HIDDEN IN THE CURRENT BUILDING
               intrRPS.clearObjective(); // CLEARS THE OBJECTIVE AFTER 3 SECONDS
            }
                break;
            case "John": // IF THE PLAYER PRESSES [E] ON JOHN
            {
               jd.startJohnsDialogue(); // CALLS JOHNS DIALOGUE METHOD
            }
                break;
            case "Car": // IF THE PLAYER PRESSES [E] ON JOHN
            {
               gm.endGame(); // CALLS THE ENDGAME METHOD
            }
                break;
        }
    }
}       
// WRITTEN BY DANIEL MARTIN