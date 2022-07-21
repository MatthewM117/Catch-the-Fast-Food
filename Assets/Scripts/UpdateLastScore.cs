using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLastScore : MonoBehaviour
{

    public Text lastScoreText;

    // Start is called before the first frame update
    void Start()
    {
        StatsData statsData = SaveData.LoadStats();
        lastScoreText.text = "Your Score: " + statsData.lastScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
