using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public float circleFallSpeed;

    [SerializeField]
    private Transform circle;

    private Vector3 screenDimensionsInWorldPoint;

    private float screenEdgeHorizontal;
    private float screenEdgeVertical;

    private float randomXPos, randomYPos;

    [SerializeField]
    private Transform canvas;

    private int nextNameNumber;

    // Start is called before the first frame update
    void Start()
    {
        circleFallSpeed = 10f;
        screenEdgeHorizontal = Camera.main.pixelWidth;
        screenEdgeVertical = Camera.main.pixelHeight;
        screenDimensionsInWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenEdgeHorizontal, screenEdgeVertical, 0));
        nextNameNumber = 0;
        SpawnCircle();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnCircle()
    {
        randomXPos = Random.Range(-screenDimensionsInWorldPoint.x, screenDimensionsInWorldPoint.x);
        randomYPos = Random.Range(1f, 2f);
        Debug.Log(randomXPos);
        GameObject newCircle = Instantiate(circle.gameObject, new Vector3(randomXPos, screenDimensionsInWorldPoint.y + randomYPos, 0), Quaternion.identity) as GameObject;
        newCircle.transform.SetParent(canvas);
        newCircle.name = "circle" + nextNameNumber;
        nextNameNumber++;
        Debug.Log(circleFallSpeed);
    }
}
