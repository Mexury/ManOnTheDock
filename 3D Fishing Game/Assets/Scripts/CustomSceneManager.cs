using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;
    public TextMeshProUGUI caught;
    public TextMeshProUGUI died;

    void Start()
    {
        Debug.Log($"S:{StatsManager.Score}, HS:{StatsManager.Highscore}");
        if (StatsManager.Score > StatsManager.Highscore)
        {
            StatsManager.Highscore = StatsManager.Score;
            Debug.Log($"NEW: S:{StatsManager.Score}, HS:{StatsManager.Highscore}");
        } else
        {
            highscore.enabled = false;
        }

        score.text = $"SCORE: {StatsManager.Score.ToString()}";
        caught.text = StatsManager.Caught.ToString();
        died.text = StatsManager.Died.ToString();
    }

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
    public static void ExitGame()
    {
        // Exit the game
        Application.Quit();
    }
}
