using UnityEngine;

public class DamagePlayer : DamageOnContact
{
    protected override void OnContact(GameObject go)
    {
        if (go.TryGetComponent(out Player player)) player.TakeDamage(Damage);
    }
}
