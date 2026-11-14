public interface IWeapon
{
    WeaponConfig WeaponConfig { get; }

    void OnUpdate(float deltaTime);
}
