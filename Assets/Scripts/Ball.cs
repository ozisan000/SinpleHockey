using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float startSpeed;
    [SerializeField]
    TextMeshPro debugText;
    Rigidbody rb;
    CollisionHandler collisionHandler;
    Sound sound;

    const string playerTag = "Player";

    Vector3 direction;
    Vector3 normal;

    public void Init(Action<Collider> trigger_enter_act,Sound sound)
    {
        rb = GetComponent<Rigidbody>();
        this.sound = sound;
        collisionHandler = GetComponent<CollisionHandler>();
        collisionHandler.collisionEnterEvent += ReflectBall;
        collisionHandler.triggerEnterEvent += trigger_enter_act;
    }

    public void Move()
    {
        Vector3 startDir = Vector3.zero;
        while (startDir.x == 0 || startDir.z == 0)
        {
            startDir.x = UnityEngine.Random.Range(-1, 2);
            startDir.z = UnityEngine.Random.Range(-1, 2);
        }
        rb.AddForce(startDir * startSpeed, ForceMode.Impulse);
    }

    public void StopMove()
    {
        rb.velocity = Vector3.zero;
        direction = Vector3.zero;
        normal = Vector3.zero;
    }

    private void Update()
    {
        direction = rb.velocity;
        debugText.text = "Velocity:"+rb.velocity;
    }

    bool PlayerUpMoveCheck(float player_speed,float ball_dir)
    {
        return player_speed > 0 && ball_dir < 0;
    }

    bool PlayerDownMoveCheck(float player_speed, float ball_dir)
    {
        return player_speed < 0 && 0 < ball_dir;
    }

    void ReflectBall(Collision collision)
    {
        normal = collision.contacts[0].normal;

        Vector3 result = Vector3.Reflect(direction, normal);

        if (collision.transform.tag == playerTag)
        {
            var playerRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            var playerMoveSpeed = playerRigidBody.velocity.normalized.z;

            if (PlayerUpMoveCheck(playerMoveSpeed, result.z) || 
                PlayerDownMoveCheck(playerMoveSpeed, result.z))
            {
                result.z = -result.z;
            }
        }

        sound.PlaySE(SEType.Reflect);

        rb.velocity = result;

        // directionÇÃçXêV
        direction = rb.velocity;
    }
}
