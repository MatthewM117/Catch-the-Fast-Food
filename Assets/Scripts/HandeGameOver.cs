using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandeGameOver : MonoBehaviour
{

    string backgroundColour;

    // Start is called before the first frame update
    void Start()
    {
        backgroundColour = PlayerPrefs.GetString("BackgroundColour");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeBackground()
    {
        if (backgroundColour == "gray")
        {
            Camera.main.backgroundColor = Color.gray;
        } else if (backgroundColour == "cyan")
        {
            Camera.main.backgroundColor = Color.cyan;
        }

    }
}
