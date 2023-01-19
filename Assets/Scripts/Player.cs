using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    JOIN_PLAYER joinPlayer = JOIN_PLAYER.Player1;
    Input input;
    Sound sound;
    Rigidbody rb;

    bool moveFlag = true;
    public bool MoveFlag { get { return moveFlag; } set { moveFlag = value; } }

    void Start()
    {
        
    }

    public void Init(Input input,Sound sound,JOIN_PLAYER join)
    {
        rb = GetComponent<Rigidbody>();
        this.input=input;
        this.sound = sound;
        joinPlayer = join;
    }

    private void FixedUpdate()
    {
        if (moveFlag)
        {
            float inputData = 0.0f;
            if (joinPlayer == JOIN_PLAYER.Player1) {
                inputData = input.Player1Input;
            }
            else
            {
                inputData = input.Player2Input;
            }
            rb.velocity = Vector3.forward * inputData * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
