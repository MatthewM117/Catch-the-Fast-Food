using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScaleObject : MonoBehaviour
{

    MoveItem2 moveItem2;
    MoveItem3 moveItem3;
    MoveItem4 moveItem4;
    MoveObstacle1 moveObstacle1;
    HandleOrange handleOrange;
    MoveObstacle2 moveObstacle2;
    MoveObstacle3 moveObstacle3;
    MoveObstacle4 moveObstacle4;

    public float growthSpeed;
    float speedIncrement = 0.0033333f;
    Vector3 newScale;
    float newHeight = 3f;

    public float circleFallSpeed = 0.01f;

    int numUntilIncreaseSpeed = 0;

    float randomBackground;

    public int score = 0;

    public int highscore = 0;

    public int numOfCirclesTapped = 227;

    public int lastScore = 0;

    string bgColour = "";

    private Vector3 screenDimensionsInWorldPoint;
    private float screenEdgeHorizontal;
    private float screenEdgeVertical;

    public float randomXPos, randomYPos;

    public GameObject gameOverScreen;
    public TextMeshProUGUI levelEndScore;
    public TextMeshProUGUI levelEndHighScore;

    float lastSpeed;
    float lastRotationSpeed;

    private AdsManager adsManager;

    bool allowRewardedAd;

    public GameObject rewardedAdButton;

    public TextMeshProUGUI watchAdText;

    private bool gameIsRunning;

    float lastPosY;
    float lastPosX;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameIsRunning = true;
        gameOverScreen.transform.localScale = new Vector3(0, 0, 0);
        adsManager = GameObject.Find("AdsManager").GetComponent<AdsManager>();
        allowRewardedAd = true;

        moveItem2 = GameObject.Find("item2").GetComponent<MoveItem2>();
        moveItem3 = GameObject.Find("item3").GetComponent<MoveItem3>();
        moveItem4 = GameObject.Find("item4").GetComponent<MoveItem4>();
        moveObstacle1 = GameObject.Find("obstacle1").GetComponent<MoveObstacle1>();
        handleOrange = GameObject.Find("item1").GetComponent<HandleOrange>();
        moveObstacle2 = GameObject.Find("obstacle2").GetComponent<MoveObstacle2>();
        moveObstacle3 = GameObject.Find("obstacle3").GetComponent<MoveObstacle3>();
        moveObstacle4 = GameObject.Find("obstacle4").GetComponent<MoveObstacle4>();

        screenEdgeHorizontal = Camera.main.pixelWidth;
        screenEdgeVertical = Camera.main.pixelHeight;
        screenDimensionsInWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenEdgeHorizontal, screenEdgeVertical, 0));

        lastPosY = transform.localPosition.y;
        lastPosX = transform.localPosition.x;

        LoadPlayerStats();

        //Camera.main.backgroundColor = Color.red;

        randomBackground = Random.Range(0f, 1f);
        if (randomBackground <= 0.2f)
        {
            Camera.main.backgroundColor = Color.gray;
            bgColour = "gray";
        }
        else if (randomBackground > 0.2f && randomBackground <= 0.4f)
        {
            Camera.main.backgroundColor = Color.cyan;
            bgColour = "cyan";
        }
        else if (randomBackground > 0.4f && randomBackground <= 0.6f)
        {
            Camera.main.backgroundColor = Color.cyan;
            bgColour = "cyan";
        }
        else if (randomBackground > 0.6f && randomBackground <= 0.8f)
        {
            Camera.main.backgroundColor = Color.cyan;
            bgColour = "cyan";
        }
        else if (randomBackground > 0.8f)
        {
            Camera.main.backgroundColor = Color.gray;
            bgColour = "gray";
        }

        PlayerPrefs.SetString("BackgroundColour", bgColour);

    } 

    // Update is called once per frame
    void Update()
    {
        if (gameIsRunning)
        {
            DetectTouch();
            Grow();
        }
        transform.localPosition = new Vector3(lastPosX, lastPosY, transform.localPosition.z);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Grow()
    {
        
        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            growthSpeed -= 0.02f;
        }*/

        
        growthSpeed += speedIncrement;

        newScale = new Vector3(0.3160938f, growthSpeed, 1f);

        transform.localScale = newScale;

        // reaches the bottom
        if (transform.localScale.y <= 0)
        {
            FindObjectOfType<AudioManager>().Play("ding");
            newHeight = Random.Range(3f, 5f);
            growthSpeed = newHeight;
            speedIncrement += 0.002f;
            circleFallSpeed += 0.002f;
            if (numUntilIncreaseSpeed == 0)
            {
                circleFallSpeed += 0.02f;
            } else if (numUntilIncreaseSpeed % 2 == 0)
            {
                circleFallSpeed += 0.01f;
                handleOrange.rotateSpeed += 0.5f;
                moveItem2.rotateSpeed += 0.5f;
                moveItem3.rotateSpeed += 0.5f;
                moveItem4.rotateSpeed += 0.5f;
                moveObstacle1.rotateSpeed += 0.5f;
                moveObstacle2.rotateSpeed += 0.5f;
                moveObstacle3.rotateSpeed += 0.5f;
                moveObstacle4.rotateSpeed += 0.5f;
            }
            numUntilIncreaseSpeed++;
            score += 1;
            Debug.Log("speed increment: " + speedIncrement);
            Debug.Log("circle fall speed: " + circleFallSpeed);
        }
        
    }

    void ReachedTop()
    {
        if (score > highscore)
        {
            highscore = score;
        }
        lastScore = score;
        SavePlayerStats();
        //LoadGameOverScene();
        GameOver();
    }

    void DetectTouch()
    {
        int i = 0;
        while (i < Input.touchCount)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), -Vector2.up);

                //Debug.Log("TAPPED");

                if (hit.collider != null)
                {
                    Touch touch = Input.GetTouch(0);
                    //Debug.Log(circleMovement.circleYpos);
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    //Debug.Log(touchPosition.y);

                    if (hit.transform.name == "item2" && touchPosition.y >= (moveItem2.circleYpos - 1f) && touchPosition.y <= (moveItem2.circleYpos + 1f))
                    {
                        //Debug.Log("LOLOLOL");
                        growthSpeed -= 0.5f;
                        moveItem2.ResetItem2();
                        FindObjectOfType<AudioManager>().Play("pop");
                    }
                    else if (hit.transform.name == "item3" && touchPosition.y >= (moveItem3.circleYpos - 1f) && touchPosition.y <= (moveItem3.circleYpos + 1f))
                    {
                        growthSpeed -= 0.5f;
                        moveItem3.ResetItem3();
                        FindObjectOfType<AudioManager>().Play("pop");
                    }
                    else if (hit.transform.name == "item4" && touchPosition.y >= (moveItem4.circleYpos - 1f) && touchPosition.y <= (moveItem4.circleYpos + 1f))
                    {
                        growthSpeed -= 0.5f;
                        moveItem4.ResetItem4();
                        FindObjectOfType<AudioManager>().Play("pop");
                    }
                    else if (hit.transform.name == "obstacle1" && touchPosition.y >= (moveObstacle1.circleYpos - 1f) && touchPosition.y <= (moveObstacle1.circleYpos + 1f))
                    {
                        growthSpeed += 3.5f;
                        moveObstacle1.ResetObstacle1();
                        FindObjectOfType<AudioManager>().Play("thud");
                    }
                    else if (hit.transform.name == "item1" && touchPosition.y >= (handleOrange.circleYpos - 1f) && touchPosition.y <= (handleOrange.circleYpos + 1f))
                    {
                        growthSpeed -= 0.5f;
                        handleOrange.ResetCircleOrange();
                        FindObjectOfType<AudioManager>().Play("pop");
                    }
                    else if (hit.transform.name == "obstacle2" && touchPosition.y >= (moveObstacle2.circleYpos - 1f) && touchPosition.y <= (moveObstacle2.circleYpos + 1f))
                    {
                        growthSpeed += 3.5f;
                        moveObstacle2.ResetObstacle2();
                        FindObjectOfType<AudioManager>().Play("thud");
                    }
                    else if (hit.transform.name == "obstacle3" && touchPosition.y >= (moveObstacle3.circleYpos - 1f) && touchPosition.y <= (moveObstacle3.circleYpos + 1f))
                    {
                        growthSpeed += 3.5f;
                        moveObstacle3.ResetObstacle3();
                        FindObjectOfType<AudioManager>().Play("thud");
                    }
                    else if (hit.transform.name == "obstacle4" && touchPosition.y >= (moveObstacle4.circleYpos - 1f) && touchPosition.y <= (moveObstacle4.circleYpos + 1f))
                    {
                        growthSpeed += 3.5f;
                        moveObstacle4.ResetObstacle4();
                        FindObjectOfType<AudioManager>().Play("thud");
                    }




                }
            }
            i++;
        }
    }
    /*
    void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScreen");
    }*/

    void SavePlayerStats()
    {
        SaveData.SaveStats(this);
    }

    void LoadPlayerStats()
    {
        StatsData statsData = SaveData.LoadStats();

        highscore = statsData.highscore;
        numOfCirclesTapped = statsData.numOfCirclesTapped;
    }

    public void GenerateRandomPos()
    {
        randomXPos = Random.Range(-screenDimensionsInWorldPoint.x, screenDimensionsInWorldPoint.x);
        randomYPos = screenDimensionsInWorldPoint.y + Random.Range(1f, 2f);
    }

    void GameOver()
    {
        adsManager.PlayAd();
        gameIsRunning = false;
        gameOverScreen.transform.localScale = new Vector3(1, 1, 1);
        if (!allowRewardedAd)
        {
            rewardedAdButton.transform.localScale = new Vector3(0, 0, 0);
            watchAdText.text = "";
        }
        levelEndScore.text = "Level: " + score.ToString();
        levelEndHighScore.text = " Highscore: " + highscore.ToString();
        lastSpeed = circleFallSpeed;
        lastRotationSpeed = handleOrange.rotateSpeed;
        circleFallSpeed = 0;
        ResetRotationSpeeds();
        Time.timeScale = 0;
    }

    void ResetRotationSpeeds()
    {
        handleOrange.rotateSpeed = 0;
        moveItem2.rotateSpeed = 0;
        moveItem3.rotateSpeed = 0;
        moveItem4.rotateSpeed = 0;
        moveObstacle1.rotateSpeed = 0;
        moveObstacle2.rotateSpeed = 0;
        moveObstacle3.rotateSpeed = 0;
        moveObstacle4.rotateSpeed = 0;
    }

    public void ContinueGame()
    {
        adsManager.PlayRewardedAd(OnRewardedAdSuccess);
    }

    void OnRewardedAdSuccess()
    {
        Time.timeScale = 1;
        gameIsRunning = true;
        circleFallSpeed = lastSpeed;
        SetRotationSpeedBack();
        gameOverScreen.transform.localScale = new Vector3(0, 0, 0);
        growthSpeed = 2f;
        allowRewardedAd = false;
    }

    void SetRotationSpeedBack()
    {
        handleOrange.rotateSpeed = lastRotationSpeed;
        moveItem2.rotateSpeed = lastRotationSpeed;
        moveItem3.rotateSpeed = lastRotationSpeed;
        moveItem4.rotateSpeed = lastRotationSpeed;
        moveObstacle1.rotateSpeed = lastRotationSpeed;
        moveObstacle2.rotateSpeed = lastRotationSpeed;
        moveObstacle3.rotateSpeed = lastRotationSpeed;
        moveObstacle4.rotateSpeed = lastRotationSpeed;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided with: " + collision.gameObject.name);

        if (collision.gameObject.tag != "tcc")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Debug.Log("ignored collision with " + collision.gameObject.name);
        }
        else
        {
            ReachedTop();
        }
        /*
        if (collision.gameObject.tag == "circleIgnore")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "tcc")
        {
            ReachedTop();
        }*/
    }
}
