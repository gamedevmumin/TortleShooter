public interface IWeaponMagazineManager
{
    bool IsReloading { get; }
    void Initialize(WeaponStats stats);
    bool IsMagazineEmpty();
    void ChangeBulletsAmountByNumber(int amount);
}
