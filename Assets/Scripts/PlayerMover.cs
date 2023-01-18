using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private float speed;
    //private PlayerInputManager inputManager;
    private Rigidbody2D rb;
    private GameManager gameManager;

    void Start()
    {
        //inputManager = gameObject.GetComponent<PlayerInputManager>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        if (gameManager.InputFlag)
        {
            //rb.velocity = inputManager.PlayerInput * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
