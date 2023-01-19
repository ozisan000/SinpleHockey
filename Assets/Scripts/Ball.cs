using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float startSpeed;
    [SerializeField]
    float speed;
    Rigidbody rb;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)) * startSpeed,ForceMode.Impulse);
    }

    //public void FixedUpdate()
    //{
    //    if (rb.velocity.magnitude < speed)
    //    {
    //        rb.velocity = new Vector3(speed * rb.velocity.x, speed * rb.velocity.y, speed * rb.velocity.z);
    //    }
    //}
}
