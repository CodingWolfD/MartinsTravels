using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSmokeParticle : MonoBehaviour
{
    private ParticleSystem smoke; // CREATES A REFERENCE TO THE SMOKE PARTICLE

    private void Awake()
    {
        smoke = this.GetComponent<ParticleSystem>(); // INITIALISES THE SMOKE PARTICLE AS THE PARTICLE SYSTEM ATTACHED TO THE GAMEOBJECT
        smoke.Pause(); // MAKES THE SMOKE PARTICLE SYSTEM PAUSE WHEN THE GAME STARTS
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player") // IF A GAMEOBJECT WITH THE TAG "PLAYER" HITS THE COLLIDER ATTACHED
        {
            StartCoroutine(playSmokeParticle()); // CALLS THE STARTCOUROUTINE METHOD TO PLAY THE SMOKE PARTICLE SYSTEM AFTER 0.3 SECONDS
        }
    }

    private IEnumerator playSmokeParticle()
    {
        yield return new WaitForSeconds(0.3f); // WAITS 0.3 SECONDS BEFORE RUNNING ANY CODE BELOW
        smoke.Play(); // PLAYS THE SMOKE PARTICLE 
    }
}
