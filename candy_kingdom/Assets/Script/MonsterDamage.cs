using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterDamage : MonoBehaviour
{
    [SerializeField] public int damage;
    public PlayerHealth playerHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.Take_Damage(damage);
        }
    }
}
