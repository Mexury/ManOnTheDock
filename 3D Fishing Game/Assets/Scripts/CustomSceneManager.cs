using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    /*
     * This is the CustomSceneManager script for managing scenes such as Victory, Loss, Main and SampleScene.
     * (I was too lazy to change the name of the SampleScene, it has no effect on the gameplay whatsoever.)
     */
    // Public variables to get in the editor.
    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;
    public TextMeshProUGUI caught;
    public TextMeshProUGUI died;

    void Start()
    {
        // When a scene loads, it checks if the current score is higher than the highscore.
        // If this is true, the highscore will then be set to whatever the current score is.
        // Thus giving you a new highscore.
        // It also disables the NEW HIGHSCORE message if you do not get a new highscore.
        if (StatsManager.Score > StatsManager.Highscore)
        {
            StatsManager.Highscore = StatsManager.Score;
        } else
        {
            highscore.enabled = false;
        }

        // Set the text in the UI to the corresponding variables.
        score.text = $"SCORE: {StatsManager.Score.ToString()}";
        caught.text = StatsManager.Caught.ToString();
        died.text = StatsManager.Died.ToString();
    }

    // Static methods to change the scene using UI buttons.
    // It has to be static, or it won't be found in the editor.
    public static void GoToMainScreen()
    {
        // Change the scene to Main scene
        SceneManager.LoadScene("Main");
    }
    public static void GoToGameScreen()
    {
        // Change the scene to SampleScene scene
        SceneManager.LoadScene("SampleScene");
    }
    public static void GoToVictoryScreen()
    {
        // Change the scene to Victory scene
        SceneManager.LoadScene("Victory");
    }
    public static void GoToLoseScreen()
    {
        // Change the scene to Loss scene
        SceneManager.LoadScene("Loss");
    }

    // A static method to exit the game using UI buttons.
    // It has to be static, or it won't be found in the editor.
    public static void ExitGame()
    {
        // Exit the game
        Application.Quit();
    }
}
