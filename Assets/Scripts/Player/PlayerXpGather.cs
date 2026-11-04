using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerXpGather : MonoBehaviour
{
    [field: SerializeField] public float XpGatherRangeBase { get; private set; } = 3f;

    CircleCollider2D _circleCollider2D;

    void Awake()
    {
        OnValidate();
    }

    void OnValidate()
    {
        if (!_circleCollider2D) _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.isTrigger = true;
        _circleCollider2D.radius = XpGatherRangeBase;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out XpItem xpItem)) xpItem.SetPullsTo(transform);
    }
}
