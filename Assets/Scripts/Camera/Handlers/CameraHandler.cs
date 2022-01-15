using Assets.Scripts.Camera.Properties;
using Assets.Scripts.Characters.Properties;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.Camera.Handlers
{
    public class CameraHandler : MonoBehaviour
    {
        #region MouseFields

        public float MouseX;
        public float MouseY;
        public float SmoothX;
        public float SmoothY;
        public float SmoothXVelocity;
        public float SmoothYVelocity;
        public float LookAngle;
        public float TitleAngle;

        #endregion
        public bool LeftPivot;
        public float Delta;
        public Transform Camera;
        public Transform Pivot;
        public Transform Character;
        public Transform CameraHolder;
        public CharacterStatus CharacterStatus;
        public CameraConfig CameraConfig;

        private void Update() {
            FixedTick();
        }

        private void FixedTick() {
            Delta = Time.deltaTime;

            HandlePosition();
            HandleRotation();

            Vector3 targetPosition = Vector3.Lerp(CameraHolder.position, Character.position, 1);
            CameraHolder.position = targetPosition;
        }

        private void HandlePosition() {
            float targetX = CameraConfig.NormalX;
            float targetY = CameraConfig.NormalY;
            float targetZ = CameraConfig.NormalZ;

            if (CharacterStatus.IsAiming) {
                targetX = CameraConfig.AimX;
                targetY = CameraConfig.AimY;
                targetZ = CameraConfig.AimZ;
            }

            if (LeftPivot) {
                targetX *= -1;
            }

            float t = Delta * CameraConfig.PivotSpeed;

            ChangePivotPosition(targetX, targetY, t);
            ChangeCameraPosition(targetX, targetY, targetZ, t);
        }

        private void ChangePivotPosition(float targetX, float targetY, float t) {
            Vector3 newPivotPosition = Pivot.localPosition;
            newPivotPosition.x = targetX;
            newPivotPosition.y = targetY;
            Pivot.localPosition = Vector3.Lerp(Pivot.localPosition, newPivotPosition, t);
        }

        private void ChangeCameraPosition(float targetX, float targetY, float targetZ, float t) {
            var newCameraPosition = new Vector3(targetX, targetY, targetZ);
            Camera.localPosition = Vector3.Lerp(Camera.localPosition, newCameraPosition, t);
        }

        private void HandleRotation() {
            MouseX = Input.GetAxis(Axis.MouseX);
            MouseY = Input.GetAxis(Axis.MouseY);

            if (CameraConfig.TurnSmooth > 0) {
                SmoothX = Mathf.SmoothDamp(SmoothX, MouseX, ref SmoothXVelocity, CameraConfig.TurnSmooth);
                SmoothY = Mathf.SmoothDamp(SmoothY, MouseY, ref SmoothYVelocity, CameraConfig.TurnSmooth);
            } else {
                SmoothX = MouseX;
                SmoothY = MouseY;
            }

            SetRotation();
        }

        private void SetRotation() {
            SetCameraHolderRotation();
            SetPivotRotation();
        }

        private void SetCameraHolderRotation() {
            LookAngle += SmoothX * CameraConfig.YRotationSpeed;
            CameraHolder.rotation = Quaternion.Euler(0, LookAngle, 0);
        }

        private void SetPivotRotation() {
            TitleAngle -= SmoothY * CameraConfig.YRotationSpeed;
            TitleAngle = Mathf.Clamp(TitleAngle, CameraConfig.MinAngle, CameraConfig.MaxAngle);
            Pivot.localRotation = Quaternion.Euler(TitleAngle, 0, 0);
        }
    }
}
