using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovementHandler : MonoBehaviour
{
    private GameManager gm;
    private Vector3 moveCircle;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectTouch();
        MoveCircle();
    }

    private void MoveCircle()
    {
        moveCircle = new Vector3(transform.localPosition.x, transform.localPosition.y - gm.circleFallSpeed, transform.position.z);
        transform.localPosition = moveCircle;

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.y < 0.0)
        {
            gm.SpawnCircle();
            DestroyCircle();
            if (gm.circleFallSpeed <= 70f)
            {
                gm.circleFallSpeed += 0.5f;
            }
        }
    }

    private void DetectTouch()
    {
        int i = 0;
        while (i < Input.touchCount)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), -Vector2.up);

                if (hit.collider != null)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    //Debug.Log(transform.position.y);
                    //Debug.Log(touchPosition.y);
                    if (hit.collider.tag == "cc" && touchPosition.y >= (transform.position.y - 3f) && touchPosition.y <= (transform.position.y + 2f)) // idk what cc means lol just a random tag
                    {
                        DestroyCircle();
                        gm.SpawnCircle();
                        //Debug.Log("tapped circle");
                        if (gm.circleFallSpeed <= 70f)
                        {
                            gm.circleFallSpeed += 0.5f;
                        } 
                    }
                }
            }
            i++;
        }
    }

    private void DestroyCircle()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "tcc")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "ps")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

}
