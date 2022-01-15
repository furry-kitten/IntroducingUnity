using UnityEngine;

namespace Assets.Scripts.Camera.Properties
{
    [CreateAssetMenu(menuName = "Camera/Config")]
    public class CameraConfig : ScriptableObject
    {
        public float TurnSmooth;
        public float PivotSpeed;
        public float XRotationSpeed;
        public float YRotationSpeed;
        public float MinAngle;
        public float MaxAngle;
        public float NormalX;
        public float NormalY;
        public float NormalZ;
        public float AimX;
        public float AimY;
        public float AimZ;
    }
}
