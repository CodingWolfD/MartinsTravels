using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private float power; // USED FOR THE POWER OF THE FLASHLIGHT
    private bool flashEquip; // BOOLEAN TO CHECK IF THE PLAYER HAS THE FLASHLIGHT EQUIPPED 

    private GameObject f_Light; // REFERENCE TO THE FLASHLIGHT POINT LIGHT
    private Slider powerBar; // REFERENCE TO THE POWERBAR COMPONENT IN THE CANVAS
    private Text powerBarText; // REFERENCE TO THE POWERBAR TEXT COMPONENT

	void Awake ()
    {
        f_Light = GameObject.Find("Flash_Light"); // FINDS THE FLASHLIGHTS LIGHT SOURCE IN GAME
        powerBar = GameObject.Find("PowerBar").GetComponent<Slider>(); // GETS THE POWERBAR COMPONENT FROM THE CANVAS
        powerBarText = GameObject.Find("PowerBarText").GetComponent<Text>(); // GETS THE POWERBARTEXT COMPONENT FROM THE CANVAS

        f_Light.gameObject.SetActive(false); // SETS THE FLASHLIGHT'S ACTIVATE STATE TO FALSE
        flashEquip = false; // SETS THE FLASHLIGHT EQUIP BOOL TO FALSE
        power = 0; // SETS THE FLASHLIGHT POWER TOO 100 WHEN THE GAME STARTS
    }

    private void Update()
    {
        checkFlash(); // CALLS THE CHECKFLASH METHOD
    }
     
    public void checkFlash()
    {
        if (flashEquip) // IF THE PLAYER HAS THE FLASH LIGHT EQUIPPED
        {
            flash_light(); // CALL THE FLASH_LIGHT METHOD
        }
    }

    private void flash_light() // ONLY RUNS IF THE PLAYER HAS THE FLASHLIGHT EQUIPPED
    {
        powerBarText.text = Mathf.FloorToInt(power).ToString() + "%"; // SETS THE FLASHLIGHT TEXT TO THE FLASHLIGHTS CURRENT POWER
        powerBar.value = Mathf.FloorToInt(power); // SET THE TEXT TO "FLASHLIGHT POWER: " + THE POWER (CONVERTED TO INT) VALUE OF THE FLASHLIGHT

        if (power > 0 && power <= 100) // IF THE FLASHLIGHT'S POWER IS OVER 0
        {
            if (Input.GetKeyDown(KeyCode.F) && f_Light.gameObject.activeSelf) // IF THE PLAYER PRESSES F WHILE THE LIGHT IS ACTIVE
            {
                f_Light.gameObject.SetActive(false); // SETS THE LIGHT'S ACTIVE STATE TO FALSE
            }
            else if (Input.GetKeyDown(KeyCode.F) && !f_Light.gameObject.activeSelf) // ELSE IF THE PLAYER PRESSES F WHILE THE LIGHT IS NOT ACTIVE
            {
                f_Light.gameObject.SetActive(true); // SETS THE LIGHT'S ACTIVE STATE TO TRUE
            }
        }

        if (power < 1) // IF THE FLASHLIGHT POWER REACHES 1 OR IS BELOW 0, SET THE POWER TO 0
        {
            power = 0; // SETS THE POWER TO EQUAL 0
        }

        if (power < 1 && f_Light.activeSelf) // IF FLASHLIGHTS POWER IS 0 AND THE LIGHT IS STILL ACTIVE, DEACTIVATE THE LIGHT
        {
            f_Light.gameObject.SetActive(false); // SETS THE LIGHTS ACTIVE STATE TO FALSE
        }

        InvokeRepeating("losePower", 10, 600); // CALL THE LOSEPOWER METHOD EVERY 600 SECONDS        
    }

    private IEnumerator losePower(int waitTime)
    {
        yield return new WaitForSeconds(waitTime); // WILL WAIT FOR THE WAITTIME TILL METHOD RUNS

        if (power > 0 && f_Light.activeSelf) // IF THE FLASHLIGHT HAS MORE POWER THAN 0 AND THE LIGHT IS ACTIVE 
        {
            power -= 1 * Time.deltaTime / 3; // TAKE ONE AWAY FROM THE FLASHLIGHTS POWER EVERY 3 SECONDS
        }
    }

    private void losePower()
    {
        StartCoroutine(losePower(10)); // START THE COROUTINE LOSEPOWER
    }

    public void setPower(int addedPower) // THIS METHOD WILL ADD THE ADDEDPOWER TO THE CURRENT POWER OF THE FLASHLIGHT 
    {
        power += addedPower;
    }

    public void setFLash(bool flash) // THIS METHOD WILL SET THE BOOL FLASHEQUIP TO THE PARAMETER FLASH (TRUE OR FALSE)
    {
        flashEquip = flash;
    }
}