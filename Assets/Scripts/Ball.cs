using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //public float ballSpeed;
    private Rigidbody2D _rb;
    private Camera _mainCam;
    private float _ballWidth;
    private GameManager _gameManagerScript;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCam = Camera.main;
        _ballWidth = this.transform.localScale.x;
        _gameManagerScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

/*
    private void CheckPosition()
    {
        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;

        float sceneRightEdge = sceneWidth/2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight/2;
        float sceneBottomEdge = sceneTopEdge * -1;

        var opposite = -rb.velocity;
        Debug.Log(opposite);

        if (transform.position.x > sceneRightEdge - ballWidth/2)
        {
            rb.AddForce(opposite);
        }
        if (transform.position.x < sceneLeftEdge + ballWidth/2)
        {
            rb.AddForce(opposite);
        }
        if (transform.position.y > sceneTopEdge - ballWidth/2)
        {
            rb.AddForce(opposite * 2, ForceMode2D.Impulse);
        }
        if (transform.position.y < sceneBottomEdge + ballWidth/2)
        {
            rb.AddForce(opposite);
        }
    }*/

    public void LaunchBall(float ballSpeed)
    {
        _rb.AddForce (Vector2.up * ballSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "BottomBorder")
        {
            _rb.velocity = Vector2.zero;
            _gameManagerScript.inPlay = false;

            //Destroy(gameObject);
        }
    }
}
