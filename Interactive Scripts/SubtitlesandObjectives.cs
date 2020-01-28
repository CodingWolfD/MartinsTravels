using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesandObjectives : MonoBehaviour
{
    private Text subtitle; // REFERENCE TO THE SUBTITLE TEXT OBJECT IN THE CANVAS
    private Text objective; // REFERENCE TO THE OBJECTIVE TEXT IN THE CANVAS
    private GameObject objBackground; // REFERENCE TO THE BACKGROUND FOR THE OBJECTIVE
    private GameObject objectiveC; // REFERENCE TO THE OBJECTIVE GAME COMPONENT

    private string subText; // USED FOR SETTING THE TEXT FOR THE SUBTITLE

    private void Awake()
    {
        subtitle = GameObject.Find("Subtitle").GetComponent<Text>(); // GETS THE SUBTITLE TEXT FROM THE CANVAS

        objBackground = GameObject.Find("objBackground"); // GETS THE OBJECTIVE BACKGROUND
        objective = GameObject.Find("Objective").GetComponent<Text>(); // GETS THE OBJECTIVE TEXT FROM THE CANVAS
        objectiveC = GameObject.Find("Objective_GameC"); // GETS THE OBJECTIVE GAME COMPONENT
    }

    private void LateUpdate()
    {
        setSub(); // CALLS THE SETSUB METHOD EVERYTIME THE LATEUPDATE METHOD RUNS
    }

    // USED FOR SUBTITLES
    private void setSub()
    {
        subtitle.text = subText; // SETS THE SUBTITLE TO SUBTEXT
    }

    public void setSub(string text)
    {
        subText = text; // USED TO SET THE NEXT SUBTITLE
    }

    public void clearSub() 
    {
        StartCoroutine(clear()); // STARTS THE COROUTINE METHOD (CLEAR())
    }

    private IEnumerator clear() // THIS IS A COROUTINE METHOD WHICH WAITS FOR 3 SECONDS THE RUNS THE CODE BELOW
    {
        yield return new WaitForSeconds((int)4); // TELL THE METHOD TO WAIT FOR 3 SECONDS BEFORE RUNNING THE INCLUDED CODE
        setSub(""); // SETS THE SUBTITLE BLANK
    }

    //---------------------------------------------------------------------------------------------------------------------------------//
    // USED FOR OBJECTIVES
    public void clearObjective() // THIS METHOD IS RUN WHEN A NEW OBJECTIVE HAS APPEARED
    {
        StartCoroutine(clearObj()); // STARTS THE COROUTINE METHOD (CLEAROBJ())
        //objBackground.SetActive(true); // ENABLES THE OBJECTIVE BACKGROUND
    }

    public void setObj(string newObj) // USED TO SET A NEW OBJECTIVE FOR THE PLAYER
    {
        objective.text = newObj; // SETS THE OBJECTIVE TEXT TO THE PARAMETER NEWOBJ
        objectiveC.GetComponent<Animator>().Play("Objective", -1, 0f); // RESETS THE ANIMATION WHEN THIS METHOD IS CALLED
    }

    private IEnumerator clearObj() // THIS IS A COROUTINE METHOD WHICH WAITS FOR 3 SECONDS THE RUNS THE CODE BELOW
    {
        yield return new WaitForSeconds((int)4); // TELL THE METHOD TO WAIT FOR 3 SECONDS BEFORE RUNNING THE INCLUDED CODE
        objectiveC.GetComponent<Animation>().Stop("Objective"); // STOPS THE ANIMATION FOR THE OBJECTIVE
        objectiveC.GetComponent<Animator>().Play("Objective_disappear"); // RESETS THE ANIMATION WHEN THIS METHOD IS 
    }
}