using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float _ballSpeed;
    private GameObject _ball;
    private Ball _ballScript;
    private GameObject _paddle;
    private Paddle _paddleScript;
    public bool inPlay;
    // Start is called before the first frame update
    void Start()
    {
        _paddle = GameObject.FindGameObjectWithTag("Paddle");
        _paddleScript = _paddle.GetComponent<Paddle>();
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _ballScript = _ball.GetComponent<Ball>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ResetBallPosition();
        PlayerInput();
    }

    //reset ball position if we hit the bottom
    private void ResetBallPosition()
    {
        if (!inPlay)
        {
            _ball.transform.position = new Vector2(_paddle.transform.position.x, _paddle.transform.position.y + _paddle.transform.localScale.y / 4);
        }

    }

    //player input to start the game
    private void PlayerInput()
    {
        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            _ballScript.LaunchBall(_ballSpeed);
        }
    }
}
