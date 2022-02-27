using UnityEngine.Events;

namespace Assets.Scripts.Base.Interfaces
{
    public interface ICharacter
    {
        void ChangeWeapon(IWeapon weapon);

        void LoseHealthPoints(float pointsLost);

        void Healing(float addedPoints);

        float DealDamage();

        void ReloadWeapon();

        void Move();

        void ControlAnimation();

        UnityEvent OnLoseHealthPoints { get; set; }

        UnityEvent OnHealing { get; set; }

        UnityEvent OnDeath { get; set; }

        UnityEvent OnChangeWeapon { get; set; }
    }
}