public interface IWeaponMagazineManager
{
    void Initialize(WeaponStats stats);
    bool IsMagazineEmpty();
    void ChangeBulletsAmountByNumber(int amount);
}
