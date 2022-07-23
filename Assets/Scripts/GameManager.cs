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
    //array of levels
    public Transform[] levels;

    //Ball references
    [SerializeField]
    private GameObject _ball;
    [SerializeField]
    private Ball _ballScript;
    //Paddle references
    [SerializeField]
    private GameObject _paddle;
    [SerializeField]
    private Paddle _paddleScript;

    //UI variables
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private int _score;
    [SerializeField]
    private Text _livesText;
    [SerializeField]
    private int _lives;
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _pauseMenu;
    
    //Values for the game continuation
    [SerializeField]
    private int _numberOfBricks;
    public int currentLevelIndex;
    [SerializeField]
    private bool _isGamePaused;


    //Values for the audio
    [HideInInspector]
    public AudioSource[] ballHitBrickAudio; //audios for the ball hitting brick
    [HideInInspector]
    public AudioSource paddleHitAudio;
    [HideInInspector]
    public AudioSource wallHitAudio;
    [HideInInspector]
    public AudioSource bottomBorderHitAudio;
    [HideInInspector]
    public AudioSource[] powerUpAudio;
    [HideInInspector]
    public AudioSource pauseAudio;

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
        _numberOfBricks --;

        if(_numberOfBricks <= 0)
        {
            if (currentLevelIndex >= levels.Length)
            {
            GameOver();
            }
            else
            {
                inPlay = false; //reseting game state to waiting.
                Invoke("LoadLevel", 1f); //changing level afte 3 second delay
            }
        }
    }

    //Power Up drop Function
    public void DropPowerUp(Transform spawnPosition)
    {
        int dropChance = Random.Range(1, 100); //chance to drop powerUP
        if (dropChance <= 50)
        {
            Instantiate(extraLifePowerUp, spawnPosition.position, spawnPosition.rotation); //creating powerup
            PlayPowerUpAppearAudio(); // playing powerup appear audio
        }
    }

    ///Functions for the audio playback
    
    public void PlayBallHitBrickSound()
    {
        AudioSource audioSource = new AudioSource();

        int n = Random.Range(0, ballHitBrickAudio.Length);

        audioSource = ballHitBrickAudio[n];

        audioSource.Play();
    }
    public void PlayWallHitAudio()
    {
        wallHitAudio.Play();
    }

    public void PlayPaddleHitAudio()
    {
        paddleHitAudio.Play();
    }

    public void PlayBottomBorderHitAudio()
    {
        bottomBorderHitAudio.Play();
    }
    
    public void PlayPowerUpAppearAudio()
    {
        powerUpAudio[0].Play();
    }
    public void PlayPowerUpGetAudio()
    {
        powerUpAudio[1].Play();
    }

    // Start is called before the first frame update
    private void Start()
    {
        LoadLevel();

        _score = 0;
        _scoreText.text = "Score: " + _score;

        _lives = 3;
        _livesText.text = "Lives: " + _lives;

        _isGamePaused = false;

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
            _ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isGamePaused = !_isGamePaused;
            PauseGame();
        }
    }

    private void GameOver()
    {
        gameOver = true;

        _gameOverPanel.SetActive(true);
    }

    private void LoadLevel()
    {
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);

        _numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;

        currentLevelIndex ++;
    }

    private void PauseGame ()
    {
        if(_isGamePaused)
        {
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
            pauseAudio.Play();
        }
        else 
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
            pauseAudio.Play();
        }
    }
}
