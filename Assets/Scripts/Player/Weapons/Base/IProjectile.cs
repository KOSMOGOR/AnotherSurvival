using UnityEngine;

public interface IProjectile
{
    Vector2 Position { get; set; }

    void SetDirection(Vector2 dir);
    void Remove();
}
