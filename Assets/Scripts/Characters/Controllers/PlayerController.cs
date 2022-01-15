using UnityEngine;

namespace Assets.Scripts.Characters.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public GameCharacter Character;

        public Transform Camera;

        public void Update() {
            if (Character != null) {
                Character.CameraTransform = Camera;
                Character.Move();
            }
        }
    }
}
