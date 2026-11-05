using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    PlayerInputInfo _inputInfo;

    public Vector2 MoveVector => _inputInfo.moveInput;
    public bool IsMoving => MoveVector.sqrMagnitude > 0;
    public Vector2 Forward { get; private set; } = Vector2.down;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (_inputInfo.moveInput.sqrMagnitude > 0) Forward = _inputInfo.moveInput;

        Vector2 targetVelocity = _inputInfo.moveInput * speed;
        rb.linearVelocity = targetVelocity;
    }

    public void ProvideInput(PlayerInputInfo inputInfo)
    {
        _inputInfo = inputInfo;
    }
}
