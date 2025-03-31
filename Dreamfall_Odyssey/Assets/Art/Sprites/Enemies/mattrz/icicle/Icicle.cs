using UnityEngine;

namespace mattrz
{
    public class Icicle : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        public LayerMask groundLayer;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().TakeDamage(1);
                DestroySelf();
            }
            else if ((groundLayer.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                DestroySelf();
            }
        }

        private void DestroySelf()
        {
            GetComponent<Rigidbody2D>().linearVelocityY = 0;
            animator.SetTrigger("destroy");
            Destroy(gameObject, 0.3f);
        }
    }
}
