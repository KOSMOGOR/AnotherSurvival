public interface IWeapon
{
    WeaponConfig WeaponConfig { get; set; }

    void OnUpdate(float deltaTime);
}
