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
    public GameObject explosion;

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

    //launching ball at the start of the game
    public void LaunchBall(float ballSpeed)
    {
        _rb.AddForce (Vector2.up * ballSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //if ball hits the bottom border
        //TODO add logic if there are several balls in the game
        if (other.tag == "BottomBorder")
        {
            _rb.velocity = Vector2.zero; //reset the ball velocity just in case

            _gameManagerScript.UpdateLive(-1); // substract live if ball hit bottom;
            _gameManagerScript.inPlay = false; //The game state into false

            //Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //if ball hit the brick
        if (other.gameObject.tag == "Brick")
        {

            _gameManagerScript.DropPowerUp(other.transform);

            //Updating the score
            _gameManagerScript.UpdateScore(other.gameObject.GetComponent<Brick>().score);
            _gameManagerScript.UpdateBricksCount();

            //Particle effects
            GameObject newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(newExplosion, 1.2f);

            //Destroying the brick
            Destroy(other.gameObject);
        }
    }
}
