using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class XpItem : MonoBehaviour
{
    [field: SerializeField] public float XpValue { get; private set; } = 20f;
    [field: SerializeField] public float PullSpeed { get; private set; } = 5f;

    Rigidbody2D _rb;

    Transform _pullsTo;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = _pullsTo
            ? (_pullsTo.position - transform.position).normalized * PullSpeed
            : Vector2.zero;

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            player.AddXp(XpValue);
            Destroy(gameObject);
        }
    }

    public void SetPullsTo(Transform pullsTo) => _pullsTo = pullsTo;

    public class Factory : PlaceholderFactory<XpItem> {}
}
