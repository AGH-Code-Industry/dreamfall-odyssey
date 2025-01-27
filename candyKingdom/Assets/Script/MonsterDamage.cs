using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    [SerializeField] public int damage;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public Animator anim;
    public MonsterMovement movement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            //anim.SetTrigger("isAttacking");

            if (movement != null)
            {
                StartCoroutine(movement.StopMovement(2));
            }

            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }
            playerHealth.Take_Damage(damage);

            
        }
    }
}
