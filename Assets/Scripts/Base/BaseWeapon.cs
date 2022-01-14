using Assets.Scripts.Base.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Base
{
    internal abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        public int Ammo;

        public int Magazine;

        public abstract float DealDamage();

        public abstract void ChangeMagazine(int ammoCount);
    }
}
