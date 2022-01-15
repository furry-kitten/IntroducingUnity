using System;
using Assets.Scripts.Base.Interfaces;
using Assets.Scripts.Characters.Properties;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Base
{
    public abstract class BaseCharacter : MonoBehaviour, ICharacter
    {
        public float HealthPoints;

        public float Vertical;

        public float Horizontal;

        public float MoveAmount;

        public IWeapon SelectedWeapon;

        public IWeapon Weapon;

        public Animator Animator;

        public CharacterStatus Status;

        public Vector3 RotationDirection;

        public Vector3 MoveDirection;

        public UnityEvent OnLoseHealthPoints { get; set; }

        public UnityEvent OnHealing { get; set; }

        public UnityEvent OnDeath { get; set; }

        public UnityEvent OnChangeWeapon { get; set; }

        public abstract void Move();

        public abstract void ControlAnimation();

        public void ChangeWeapon(IWeapon weapon) {
            SelectedWeapon = weapon;
            OnChangeWeapon?.Invoke();
        }

        public void LoseHealthPoints(float pointsLost) {
            HealthPoints -= pointsLost;
            Status.IsAlive = HealthPoints > 0;
            if (Status.IsAlive == false) {
                OnDeath?.Invoke();
            }
            OnLoseHealthPoints?.Invoke();
        }

        public void Healing(float addedPoints) {
            HealthPoints += addedPoints;
            OnHealing?.Invoke();
        }

        public float DealDamage() => Weapon.DealDamage();

        public void ReloadWeapon() {

        }
    }
}
