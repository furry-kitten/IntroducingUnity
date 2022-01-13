using UnityEngine;

namespace Assets.Scripts.Characters.Properties
{
    [CreateAssetMenu(menuName = "Properties/Status")]
    public class CharacterStatus : ScriptableObject
    {
        public bool IsAiming;
        public bool IsSprinting;
        public bool IsGround;
        public bool IsAlive;
        public float SpeedForward;
        public float SpeedRight;

        public float Speed { get; set; }
    }
}