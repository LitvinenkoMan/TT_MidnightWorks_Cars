using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController controller;
        
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Update()
        {
            if (controller.velocity.magnitude > 0.01f)
            {
                var rot = Quaternion.LookRotation(controller.velocity);
                Quaternion targetRotation = new Quaternion(0, rot.y, 0, rot.w);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            animator.SetFloat(Speed, 1-controller.velocity.magnitude);
        }
    }
}
