using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{

    ScaleObject scaleObject;
    Material myMaterial;

    float circleSpeed = 4.45f; // circle starting position, ik its called circle speed but its also for location lol
    Vector3 moveCircle;
    float randomXPos;
    //float fallSpeed = 0.01f;

    public float circleYpos;

    // Start is called before the first frame update
    void Start()
    {
        randomXPos = Random.Range(-2f, 2f);
        circleSpeed = Random.Range(5f, 7f);

        scaleObject = GameObject.Find("Parent Square").GetComponent<ScaleObject>();
        myMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCircle();
    }

    void MoveCircle()
    {
        circleSpeed -= scaleObject.circleFallSpeed;
        moveCircle = new Vector3(randomXPos, circleSpeed, transform.position.z);
        transform.localPosition = moveCircle;

        if (transform.localPosition.y <= -5.25f)
        {
            ResetCircle();
        }

        circleYpos = transform.localPosition.y;
    }

    public void ResetCircle()
    {
        circleSpeed = Random.Range(5f, 7f); // reset location
        randomXPos = Random.Range(-2f, 2f);
        Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        myMaterial.color = newColor;
    }
}
