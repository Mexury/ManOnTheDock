using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatsManager
{
    // Private variables for the properties to attach to.
    private static int _alive;
    private static int _caught;
    private static int _died;
    private static int _score;
    private static int _highscore;

    // Public properties which are connected to the private variables above.
    // We use properties because we can provide functionality when you get or set it.
    // In this case; when a property is set, we check if the value is less than 0. If it is we will set the value to at least 0.
    public static int Alive
    {
        get { return _alive; }
        set { if (value < 0) value = 0; _alive = value; }
    }
    public static int Caught
    {
        get { return _caught; }
        set { if (value < 0) value = 0; _caught = value; }
    }
    public static int Died
    {
        get { return _died; }
        set { if (value < 0) value = 0; _died = value; }
    }
    public static int Score
    {
        get { return _score; }
        set { if (value < 0) value = 0; _score = value; }
    }
    public static int Highscore
    {
        get { return _highscore; }
        set { if (value < 0) value = 0; _highscore = value; }
    }

    // A reset method that just resets every property except for the highscore.
    public static void Reset()
    {
        Alive = 0;
        Caught = 0;
        Died = 0;
        Score = 0;
    }

    // A reset method for resetting the highscore property specifically.
    public static void ResetHighscore()
    {
        Highscore = 0;
    }

}
