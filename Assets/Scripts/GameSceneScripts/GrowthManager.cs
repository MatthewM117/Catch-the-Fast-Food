using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    [HideInInspector]
    public float growthSpeed;
    private float speedIncrement;
    private Vector3 newScale;
    private float newHeight;
    private int numUntilIncreaseSpeed;
    [HideInInspector]
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        speedIncrement = 1.0f;
        newHeight = 3.0f;
        numUntilIncreaseSpeed = 0;
        score = 0;
        growthSpeed = 142.2933f;
    }

    // Update is called once per frame
    void Update()
    {
        Grow();
        CheckIfBottomReached();
    }

    void Grow()
    {

        growthSpeed += speedIncrement;

        newScale = new Vector3(44.97804f, growthSpeed, 142.2933f);

        transform.localScale = newScale;

        /*
        // reaches the bottom
        if (pos.y < 0.0)
        {
            newHeight = Random.Range(3f, 5f);
            growthSpeed = newHeight;
            speedIncrement += 0.002f;
            circleFallSpeed += 0.002f;
            if (numUntilIncreaseSpeed == 0)
            {
                circleFallSpeed += 0.02f;
            }
            else if (numUntilIncreaseSpeed % 2 == 0)
            {
                circleFallSpeed += 0.01f;
            }
            numUntilIncreaseSpeed++;
            score += 1;

        }

        // reaches the top
        if (pos.y > 1.0)
        {
            Debug.Log("at top");
        }*/


    }

    // reaches the top. losing
    private void ReachedTop()
    {
        Debug.Log("reached top");
        /*
        if (score > highscore)
        {
            highscore = score;
        }
        lastScore = score;
        SavePlayerStats();
        LoadGameOverScene();*/
    }

    private void ReachedBottom()
    {
        Debug.Log("reached bottom");
    }

    private void CheckIfBottomReached()
    {
        if (transform.localScale.y <= 0)
        {
            ReachedBottom();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "tcc")
        {
            ReachedTop();
        }
    }
}
