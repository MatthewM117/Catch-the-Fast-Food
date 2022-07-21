using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    ScaleObject scaleObject;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scaleObject = GameObject.Find("Parent Square").GetComponent<ScaleObject>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Level:" + scaleObject.score.ToString();
    }
}
