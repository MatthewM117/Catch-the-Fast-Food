using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData
{
    public int highscore;
    public int numOfCirclesTapped;
    public int lastScore;

    public StatsData(ScaleObject scaleObject)
    {
        highscore = scaleObject.highscore;
        numOfCirclesTapped = scaleObject.numOfCirclesTapped;
        lastScore = scaleObject.lastScore;
    }
}
