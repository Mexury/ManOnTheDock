using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatsManager
{
    private static int _alive;
    private static int _caught;
    private static int _died;
    private static int _score;
    private static int _highscore;

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

    public static void Reset()
    {
        Alive = 0;
        Caught = 0;
        Died = 0;
        Score = 0;
    }

    public static void ResetHighscore()
    {
        Highscore = 0;
    }

}
