using UnityEngine;

public abstract class DamageOnContact : MonoBehaviour
{
    [field: SerializeField] public float Damage { get; private set; }

    void OnTriggerEnter2D(Collider2D collider)
    {
        OnContact(collider.gameObject);
    }

    abstract protected void OnContact(GameObject go);
}
