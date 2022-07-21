using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle3 : MonoBehaviour
{
    ScaleObject scaleObject;
    //Material myMaterial;

    float circleSpeed = 4.45f; // circle starting position, ik its called circle speed but its also for location lol
    Vector3 moveCircle;
    float randomXPos;
    //float fallSpeed = 0.01f;
    public float rotateSpeed;

    public float circleYpos;

    //string spriteName = "";
    //int theme;
    //SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        randomXPos = Random.Range(-2f, 2f);
        circleSpeed = Random.Range(5f, 7f);
        rotateSpeed = 1f;

        scaleObject = GameObject.Find("Parent Square").GetComponent<ScaleObject>();
        //myMaterial = GetComponent<Renderer>().material;

        //theme = PlayerPrefs.GetInt("Themes");
        /*
        SelectSprite();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(spriteName);*/
    }

    // Update is called once per frame
    void Update()
    {
        MoveCircle();
    }
    /*
    void SelectSprite()
    {
        if (theme == 1)
        {
            spriteName = "steak";
        }
        else if (theme == 2)
        {
            spriteName = "circle";
        }
    }*/

    void MoveCircle()
    {
        transform.Rotate(Vector3.back * rotateSpeed);

        circleSpeed -= scaleObject.circleFallSpeed;
        moveCircle = new Vector3(randomXPos, circleSpeed, transform.position.z);
        transform.localPosition = moveCircle;

        if (transform.localPosition.y <= -5.25f)
        {
            ResetObstacle3();
        }

        circleYpos = transform.localPosition.y;
    }

    public void ResetObstacle3()
    {
        scaleObject.GenerateRandomPos();
        circleSpeed = scaleObject.randomYPos;
        randomXPos = scaleObject.randomXPos;
    }
}
