using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Collision> collisionEnterEvent;

    public event Action<Collision> collisionExitEvent;

    public event Action<Collision> collisionStayEvent;

    public event Action<Collider> triggerEnterEvent;

    public event Action<Collider> triggerExitEvent;

    public event Action<Collider> triggerStayEvent;

    public event Action<Collision2D> collisionEnter2DEvent;

    public event Action<Collision2D> collisionExit2DEvent;

    public event Action<Collision2D> collisionStay2DEvent;

    public event Action<Collider2D> triggerEnter2DEvent;

    public event Action<Collider2D> triggerExit2DEvent;

    public event Action<Collider2D> triggerStay2DEvent;

    public event Action<ControllerColliderHit> controllerColliderHit;
    private void OnCollisionEnter(Collision collision)
    {
        collisionEnterEvent?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionExitEvent?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        collisionStayEvent?.Invoke(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerEnterEvent?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        triggerExitEvent?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        triggerStayEvent?.Invoke(other);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        controllerColliderHit?.Invoke(hit);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionEnter2DEvent?.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionExit2DEvent?.Invoke(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collisionStay2DEvent?.Invoke(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerEnter2DEvent?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerExit2DEvent?.Invoke(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        triggerStay2DEvent?.Invoke(collision);
    }
    //private void Update()
    //{
    //    Debug.Log(collisionService == null);
    //}
}
