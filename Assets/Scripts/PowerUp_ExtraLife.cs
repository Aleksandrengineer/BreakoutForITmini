using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_ExtraLife : MonoBehaviour
{
    //speed of the power up moving down
    public float speed = 0.5f;
    [SerializeField]
    private Camera _mainCam;
    [SerializeField]
    private GameManager _gameManagerScript;
    void Start()
    {
        _mainCam = Camera.main;

        _gameManagerScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovingDown();
        
        CheckPosition();
    }
    private void MovingDown()
    {
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed);
    }

    private void CheckPosition()
    {
        float sceneHeight = _mainCam.orthographicSize * 2;

        float sceneTopEdge = sceneHeight/2;
        float sceneBottomEdge = sceneTopEdge * -1;

        if (transform.position.y < sceneBottomEdge)
        {
            Destroy(this.gameObject);
        }
    }
}
