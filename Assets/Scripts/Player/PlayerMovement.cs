using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    PlayerInputInfo _inputInfo;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = _inputInfo.moveInput * speed;
        rb.linearVelocity = targetVelocity;
    }

    public void ProvideInput(PlayerInputInfo inputInfo)
    {
        _inputInfo = inputInfo;
    }
}
