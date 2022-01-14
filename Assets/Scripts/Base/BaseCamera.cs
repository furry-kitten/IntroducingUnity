using Assets.Scripts.Base.Interfaces;
using Assets.Scripts.Camera.Handlers;
using Assets.Scripts.Camera.Properties;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Assets.Scripts.Base
{
    public class BaseCamera : MonoBehaviour, ICamera
    {
        public Vector3 Pivot;

        public Vector3 TargetLook;

        public Transform Camera;

        public CameraConfig Config;

        public CameraHandler Handler = new CameraHandler();
    }
}
