using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //ball speed so you can adjust it
    public float _ballSpeed;
    //inPlay bool so we can control our game state
    public bool inPlay;
    public bool gameOver = false;
    //prefabs references
    public GameObject extraLifePowerUp;

    //Ball references
    private GameObject _ball;
    private Ball _ballScript;
    //Paddle references
    private GameObject _paddle;
    private Paddle _paddleScript;

    //UI variables
    private Text _scoreText;
    private int _score;
    private Text _livesText;
    private int _lives;
    [HideInInspector]
    public GameObject gameOverPanel;

    private int numberOfBricks;

    public void UpdateLive(int liveValue)
    {
        _lives += liveValue;

        if (_lives <= 0)
        {
            _lives = 0;
            GameOver();
        }

        _livesText.text = "Lives: " + _lives;
    }

    public void UpdateScore(int scoreValue)
    {
        _score += scoreValue;
        _scoreText.text = "Score: " + _score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateBricksCount()
    {
        numberOfBricks --;

        if(numberOfBricks <= 0)
        {
            GameOver();
        }
    }

    //Power Up drop Function
    public void DropPowerUp(Transform spawnPosition)
    {
        int dropChance = Random.Range(1, 100);
        if (dropChance <= 50)
        {
            Instantiate(extraLifePowerUp, spawnPosition.position, spawnPosition.rotation);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _paddle = GameObject.FindGameObjectWithTag("Paddle");
        _paddleScript = _paddle.GetComponent<Paddle>();

        _ball = GameObject.FindGameObjectWithTag("Ball");
        _ballScript = _ball.GetComponent<Ball>();

        _scoreText = GameObject.FindGameObjectWithTag("Score Text").GetComponent<Text>();
        _score = 0;
        _scoreText.text = "Score: " + _score;

        _livesText = GameObject.FindGameObjectWithTag("Lives Text").GetComponent<Text>();
        _lives = 3;
        _livesText.text = "Lives: " + _lives;

        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    // Update is called once per frame
    private void Update()
    {
        ResetBallPosition();
        PlayerInput();
    }

    //reset ball position if we hit the bottom
    private void ResetBallPosition()
    {
        if (gameOver)
        {
            return;
        }

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

    private void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }
}
