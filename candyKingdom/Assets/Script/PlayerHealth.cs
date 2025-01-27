using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int health;
    public Animator anim;
    public PlayerMovement movement;
    public MonsterMovement monmovement;

    void Start()
    {
        health = maxHealth;
        anim.SetBool("isDead", false);
        anim.SetBool("isHurt", false);
        movement = GetComponent<PlayerMovement>();
        monmovement = GetComponent<MonsterMovement>();
    }

    public void Take_Damage(int damage)
    {
        StartCoroutine(HandleDamage(damage));
    }

    public IEnumerator HandleDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("isHurt");
        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            //movement.enabled = false;
            //monmovement.enabled = false;
            yield return new WaitForSeconds(2f);
            //Destroy(gameObject);
            movement.transform.position = movement.lastCheckpointPosition; 
            health = maxHealth; 
            anim.SetBool("isDead", false);
        }
    }
}
