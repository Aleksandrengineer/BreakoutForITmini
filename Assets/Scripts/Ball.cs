using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Camera _mainCam;
    [SerializeField]
    private float _ballWidth;
    [SerializeField]
    private GameManager _gameManagerScript;


    void Start()
    {
        _ballWidth = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //launching ball at the start of the game
    public void LaunchBall(float ballSpeed)
    {
        _rb.AddForce (Vector2.up * ballSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "BottomBorder")
        {
            _gameManagerScript.UpdateLive(-1); // substract live if ball hit bottom;
            
            _gameManagerScript.inPlay = false; //The game state into false

            _gameManagerScript.PlayBottomBorderHitAudio();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Brick")
        {
            //Referencing brickscript
            Brick brickScript = other.gameObject.GetComponent<Brick>();

            //Check wether to break the brick or not
            if (brickScript.hitsToBreak > 1)
            {
                brickScript.BreakBrick();
            }

            else
            {
                //check wether to drop powerup or not
                _gameManagerScript.DropPowerUp(other.transform);

                //Updating the score
                _gameManagerScript.UpdateScore(brickScript.score);

                //Update brick count
                _gameManagerScript.UpdateBricksCount();

                //Particle effects
                brickScript.PlayParticleEffect();

                //Destroying the brick
                Destroy(other.gameObject);
            }
            _gameManagerScript.PlayBallHitBrickSound();
        }

        if (other.gameObject.tag == "Paddle")
        {
            _gameManagerScript.PlayPaddleHitAudio();
        }

        if (other.gameObject.tag == "Border")
        {
            _gameManagerScript.PlayWallHitAudio();
        }
    }
}
