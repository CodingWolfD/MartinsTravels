using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private Image splashImage; // CREATES A REFERENCE TO THE SPLASHIMAGE 
    private int levelToLoad; // CREATES A NEW INTEGER TO STORE WHAT LEVEL WE WANT TO LOAD 

    private IEnumerator Start()
    {
        levelToLoad = 1; // SETS THE LEVEL WE WANT TO LOAD TO 1
        splashImage.canvasRenderer.SetAlpha(0.0f); // SETS THE ALPHA OF THE SPLASHIMAGE TO 0

        fadeIn(); // CALLS THE FADE IN METHOD TO FADE THE SPLASHIMAGE IN
        yield return new WaitForSeconds(4); // TELLS THE METHOD WAIT FOR 4 SECONDS
        fadeOut(); // CALLS THE FADE OUT METHOD TO FADE THE SPLASHIMAGE OUT
        yield return new WaitForSeconds(4); // TELLS THE METHOD TO WAIT FOR 4 SECONDS
        SceneManager.LoadScene(levelToLoad); // TELL THE GAME TO LOAD THE NEXT LEVEL
    }

    private void fadeIn()
    {
        splashImage.CrossFadeAlpha(1, 5, true); // TWEENS BETWEEN 0 AND 1 FOR THE APLHA FOR 5 SECONDS AND USES THE TIMESCALE
    }

    private void fadeOut()
    {
        splashImage.CrossFadeAlpha(0, 5, true); // TWEENS BETWEEN 1 AND 0 FOR THE APLHA FOR 5 SECONDS AND USES THE TIMESCALE
    } 
}