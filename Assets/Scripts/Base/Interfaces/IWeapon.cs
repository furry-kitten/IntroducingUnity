namespace Assets.Scripts.Base.Interfaces
{
    public interface IWeapon
    {
        float DealDamage();

        void ChangeMagazine(int ammoCount);
    }
}
