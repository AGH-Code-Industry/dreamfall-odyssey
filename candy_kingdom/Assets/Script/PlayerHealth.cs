using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int health;
    public Animator anim;

    void Start()
    {
        health = maxHealth;
        anim.SetBool("isDead", false);
        anim.SetBool("isHurt", false);
    }

    public void Take_Damage(int damage)
    {
        StartCoroutine(HandleDamage(damage));
    }

    public IEnumerator HandleDamage(int damage)
    {
        health -= damage;
        anim.SetBool("isHurt", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isHurt", false);
        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}
