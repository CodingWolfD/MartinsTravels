using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JohnsDialogue : MonoBehaviour
{
    private TextAsset dialogueText; // CREATES A REFERENCE TO THE TEXT FILE THAT HOLDS THE DIALOGUE FOR JOHN
    private string str; // CREATES A NEW STRING TO STORE THE CHARATERS BEING DISPLAYED

    private string[] dialogueStrings; // CREATES AN ARRAY THAT STORES ALL THE STRING FOUND IN THE TEXT FILE
    private string newDialogue; // CREATES A NEW STRING THAT STORES THE CURRENT DIALOGUE STRINFG

    public SubtitlesandObjectives subtitles; // CREATES A REFERENCE TO THE SUBTITLES GAME OBJECT

    private void Awake()
    {
        loadTextFile(); // CALLS THE LOADTEXTFILE METHOD WHEN THE GAME STARTS
    }


    private void loadTextFile()
    {
        dialogueText = Resources.Load("Text_Files/DialogueText") as TextAsset; // SETS THE DIALOGUETEXT TEXTASSET TO THE TEXT FILE AT THE PATH SPECIFIED

        if(dialogueText != null) // IF THE DIALOGUETEXT FILE IS NOT EQUAL TO NULL
        {
            Debug.Log("Dialogue File Found"); // PRINTS TO THE CONSOLE THAT THE DIALOGUE FILE WAS FOUND
        }
        else // IF THE DIALOGUETEXT FILE IS NULL
        {
            Debug.Log("Text file could not be located"); // PRINTS TO THE CONSOLE THAT THE DIALOGUE FILE WASN'T FOUND
        }

        dialogueStrings = (dialogueText.text.Split('\n')); // SETS THE DIALOGUE STRINGS ARRAY TO THE TEXT FILES STRINGS AND SPLITS THEM ON A NEW LINE
    }

    public void startJohnsDialogue()
    {
        StartCoroutine(johnsDialogue()); // CALLS THE IENUMERATOR CALLED "JOHNSDIALOGUE"
    }

    private IEnumerator johnsDialogue()
    {
        int i = 0; // CREATES A NEW INT CALLED "i" AND INITIALISES IT AS 0
        str = ""; // SETS THE STR TO NOTHING

        for (int j = 0; j < dialogueStrings.Length; j++) // CREATES A NEW FOR LOOP THAT RUNS UNTIL J IS EQUAL TO THE AMOUNT OF STRINGS STORED IN THE ARRAY
        {
            newDialogue = dialogueStrings[j]; // SETS THE NEWDIALOGUE STRING TO THE CURRENT CURRENT INDEX IN THE STRING ARRAY
            str = ""; // RESETS THE STR STRING
            i = 0; // RESETS THE i INT

            while (i < newDialogue.Length) // THIS RUNS WHILE i IS LESS THAT THE NEWDIALOGUE'S AMOUNT OF CHARACTERS
            {
                str += newDialogue[i++]; // ADDS EVERY CHARACTER THATS STORED IN THE NEWDIALOGUE TO THE STR STRING
                subtitles.setSub(str); // SETS THE SUBTITLES GAMEOBJECT TO THE CURRENT STR STRING
                yield return new WaitForSeconds(0.1f); // WAITS FOR 0.01 SECOND AND CONTINUES THE LOOP
            }
        }

        subtitles.clearSub(); // ONCE J IS EQUAL TO THE AMOUNT OF STRINGS STORED IN THAT ARRAY, CALLS THE CLEAR SUB METHOD INSIDE THE SUBTITLES CLASS
    }
}