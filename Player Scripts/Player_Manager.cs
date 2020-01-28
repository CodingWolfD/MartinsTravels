using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public int health; // REFERENCE FOR THE PLAYERS HEALTH

    private Slider healthBar; // REFERENCE TO THE SLIDER USED FOR THE PLAYERS HEALTHBAR
    private Text healthText; // REFERENCE FOR THE TEXT USED TO TELL THE PLAYER WHAT THE CURRENT HEALTH IS

	void Start ()
    {
        health = 100; // SETS HEALTH TO 100%

        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>(); // GETS THE HEALTHBAR FROM THE CANVAS
        healthText = GameObject.Find("HealthText").GetComponent<Text>(); // GETS THE HEALTHTEXT FROM THE CANVAS
	}
	
	void Update ()
    {
        updateHealthBar(); // CALLS THE UPDATEHEALTHBAR METHOD EVERY UPDATE
	}

    private void minusHealth(int value) // THIS METHOD WILL BE USED TO TAKE AWAY HEALTH WHEN THE PLAYER COLLIDES WITH AN ENEMY
    {
        health -= value;  // HEALTH IS EQUAL TO THE CURRENT HEALTH - VALUE
    }

    private void updateHealthBar() // THIS METHOD IS USED TO CONSTANTLY UPDATE THE HEALTHBAR IN-GAME
    {
        healthBar.value = health; // HEALTHBAR SLIDER IS EQUAL TO THE PLAYERS CURRENT HEALTH
        healthText.text = "Health: " + health + "%"; // SETS THE HEALTHBAR TEXT TO THE CURRENT HEALTH + "%"
    }
}
// WRITTEN BY DANIEL MARTIN