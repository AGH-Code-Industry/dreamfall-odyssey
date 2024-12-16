using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace mattrz
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int health = 3;
        [SerializeField] private float speed = 8f;
        [SerializeField] private float jumpForce = 20f;
        [SerializeField] private float invincibilityDuration = 1f;
        [SerializeField] private float flashInterval = 0.1f;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private Animator animator;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private ContactFilter2D GroundContactFilter;

        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsInAir = Animator.StringToHash("IsInAir");

        private float _horizontalVelocity;
        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;

        private bool IsGrounded => _rigidbody.IsTouching(GroundContactFilter);
        private bool isInvincible = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            _rigidbody.linearVelocity = new Vector2(_horizontalVelocity, _rigidbody.linearVelocity.y);

            animator.SetBool(IsInAir, !IsGrounded);

            if (IsGrounded)
            {
                _rigidbody.gravityScale = 20f;
            }
            else
            {
                _rigidbody.gravityScale = 4f;
            }
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
            }
            else if (_horizontalVelocity < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            animator.SetBool(IsRunning, Mathf.Abs(_horizontalVelocity) > 0.01f);
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && IsGrounded)
            {
                _rigidbody.linearVelocityY = jumpForce;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collider.IsTouchingLayers(enemyLayer))
            {
                if (other.contacts[0].normal.y > 0.5f)
                {
                    Destroy(other.gameObject);
                    _rigidbody.linearVelocityY = jumpForce;
                }
                else
                {
                    if (isInvincible) return;
                    health--;
                    Debug.Log("Health: " + health);
                    if (health <= 0)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        StartCoroutine(InvincibilityCoroutine());
                    }
                }
            }
        }

        private IEnumerator InvincibilityCoroutine()
        {
            isInvincible = true;
            float elapsedTime = 0f;

            while (elapsedTime < invincibilityDuration)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(flashInterval);
                elapsedTime += flashInterval;
            }

            spriteRenderer.enabled = true;
            isInvincible = false;
        }
    }
}