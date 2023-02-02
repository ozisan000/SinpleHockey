using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;                //プレイヤーのスピード
    float inputKeyValue = 0.0f;
    Rigidbody rb;

    public void Init()
    {
        rb = GetComponent<Rigidbody>();
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

    private void Update()
    {
        rb.velocity = Vector3.forward * inputKeyValue * speed;
    }
}
