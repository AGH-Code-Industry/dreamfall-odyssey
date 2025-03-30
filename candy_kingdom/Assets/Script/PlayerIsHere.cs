using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerIsHere : MonoBehaviour
{
    [SerializeField] public int damage;
    public PlayerHealth playerHealth;
    public Animator anim;
    public Transform player;

    [SerializeField] private float detectionRadius = 10f;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            anim.SetBool("isPlayer", true);
        }
        else
        {
            anim.SetBool("isPlayer", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.Take_Damage(damage);
        }
    }
}
