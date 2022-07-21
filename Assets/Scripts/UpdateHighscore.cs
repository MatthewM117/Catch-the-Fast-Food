using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHighscore : MonoBehaviour
{

    public Text highscoreText;
    // Start is called before the first frame update
    void Start()
    {
        StatsData statsData = SaveData.LoadStats();
        highscoreText.text = "Highscore: " + statsData.highscore.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
