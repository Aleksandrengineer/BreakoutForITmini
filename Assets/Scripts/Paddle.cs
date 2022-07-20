using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float paddleSpeed;
    private Camera _mainCam;
    private float _paddleWidth;
    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
        _paddleWidth = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        PaddleMovement();

    }

    private void PaddleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate (Vector2.right * horizontal * Time.deltaTime * paddleSpeed);
        CheckPosition();
    }

    private void CheckPosition()
    {
        //this checking for the screen border considering th border of the camera size
        float sceneWidth = _mainCam.orthographicSize * 2 * _mainCam.aspect;

        float sceneRightEdge = sceneWidth/2;
        float sceneLeftEdge = sceneRightEdge * -1;

        if (transform.position.x > sceneRightEdge - _paddleWidth/2)
        {
            transform.position = new Vector2(sceneRightEdge - _paddleWidth/2, transform.position.y);
        }
        if (transform.position.x < sceneLeftEdge + _paddleWidth/2)
        {
            transform.position = new Vector2(sceneLeftEdge + _paddleWidth/2, transform.position.y);
        }
    }
}
