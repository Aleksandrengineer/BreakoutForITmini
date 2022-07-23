using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float paddleSpeed;
    [SerializeField]
    private Camera _mainCam;
    [SerializeField]
    private float _paddleWidth;
    [SerializeField]
    private GameManager _gameManagerScript;

    void Start()
    {
        _paddleWidth = this.transform.localScale.x;
    }

    void Update()
    {
        PaddleMovement();
    }

    //Player input to control paddle
    private void PaddleMovement()
    {
        if(_gameManagerScript.gameOver)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate (Vector2.right * horizontal * Time.deltaTime * paddleSpeed);

        CheckPosition();
    }

    //clamp the paddle between screen size
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        //logic for the extra life power up
        if (other.tag == "Extra Life")
        {
            _gameManagerScript.UpdateLive(1); // incrementing life
            
            _gameManagerScript.PlayPowerUpGetAudio(); //play powerup get audio

            Destroy(other.gameObject); //deleting the powerUP
        }
    }
}
