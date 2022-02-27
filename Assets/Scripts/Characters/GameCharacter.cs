using System;
using Assets.Scripts.Base;
using Assets.Scripts.Constants;

using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class GameCharacter : BaseGameCharacter
    {
        private const float DampTime = 0.15f;

        public Transform CameraTransform { get; set; }

        public override void Move()
        {
            ControlAnimation();
            SetMovement();
            RotationNormal();
        }

        public override void ControlAnimation()
        {
            Vertical = Input.GetAxis(Axis.Vertical);
            Horizontal = Input.GetAxis(Axis.Horizontal);
            MoveAmount = Mathf.Clamp01(Mathf.Abs(Vertical) + Mathf.Abs(Horizontal));
            Animator.SetFloat(nameof(Vertical), Vertical, DampTime, Time.deltaTime);
        }

        public void SetMovement()
        {
            Vector3 move = CameraTransform.forward * Vertical;
            move += CameraTransform.right * Horizontal;
            MoveDirection = move * Status.SpeedForward * Time.deltaTime;
            RotationDirection = CameraTransform.forward;
            transform.localPosition += MoveDirection;
        }

        public void RotationNormal()
        {
            if (Status.IsAiming)
            {
                RotationDirection = MoveDirection;
            }

            Vector3 targetDirection = RotationDirection;
            targetDirection.y = 0;
            if (targetDirection == default)
            {
                targetDirection = transform.forward;
            }

            Quaternion lookDirection = Quaternion.LookRotation(targetDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, lookDirection, 1);
            transform.rotation = targetRotation;
        }
    }
}
