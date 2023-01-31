using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float startSpeed;
    [SerializeField]
    float reflectRatio = 10.0f;
    Rigidbody rb;
    CollisionHandler collisionHandler;

    const string playerTag = "Player";

    Vector3 direction;
    Vector3 normal;

    public void Init(Action<Collider> trigger_enter_act)
    {
        rb = GetComponent<Rigidbody>();
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
    }

    void ReflectBall(Collision collision)
    {
        normal = collision.contacts[0].normal;

        Vector3 result = Vector3.Reflect(direction, normal);

        if (collision.transform.tag == playerTag)
        {
            Debug.Log("Before result:" + result);
            var playerRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidBody.velocity.normalized.z != 0)
                //result += playerRigidBody.velocity.normalized * reflectRatio;
                result += playerRigidBody.velocity;
            Debug.Log("Affter result:" + result);
        }

        rb.velocity = result;

        // directionÇÃçXêV
        direction = rb.velocity;
    }
}
