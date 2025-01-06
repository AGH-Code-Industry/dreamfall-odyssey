using System;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    private int playerHealth = 1;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth < 0)
        {
            Death();
            //respawn
        }
    }

    public void KillPlayer()
    {
        Death();
    }

    private void Death()
    {
        
        playerMovement.PlayerDeathMovement();
    }
}
