using UnityEngine;
using UnityEngine.InputSystem;

namespace mattrz
{
    public class Player : MonoBehaviour
    {

        [SerializeField] private float speed = 8f;
        [SerializeField] private float jumpForce = 20f;
        [SerializeField] private Animator animator;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckRadius = 0.6f;

        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsInAir = Animator.StringToHash("IsInAir");
    
        private float _horizontalVelocity;
        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
        }
        
        private void Update()
        {
            _rigidbody.linearVelocity = new Vector2(_horizontalVelocity, _rigidbody.linearVelocity.y);

            animator.SetBool(IsInAir, !IsGrounded());
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            _horizontalVelocity = ctx.ReadValue<float>() * speed;
            if (Mathf.Abs(_horizontalVelocity) < 0.01f)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            
            if (!animator) return;
            if (_horizontalVelocity > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            } else if (_horizontalVelocity < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            animator.SetBool(IsRunning, Mathf.Abs(_horizontalVelocity) > 0.01f);
        }
        
        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        
        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - _collider.size.y/2), groundCheckRadius, groundLayer);
        }
    }
}