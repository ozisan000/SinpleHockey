using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;                //プレイヤーのスピード
    float inputKeyValue = 0.0f;
    Sound sound;
    Rigidbody rb;

    bool moveFlag = true;
    public bool MoveFlag { get { return moveFlag; } set { moveFlag = value; } }

    public void Init(Sound sound)
    {
        rb = GetComponent<Rigidbody>();
        this.sound = sound;
    }

    public void InputKey(float value)
    {
        inputKeyValue = value;
    }

    public void StopMove()
    {
        rb.velocity = Vector3.zero;
        inputKeyValue = 0.0f;
    }

    private void FixedUpdate()
    {
        if (moveFlag)
        {
            rb.velocity = Vector3.forward * inputKeyValue * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
