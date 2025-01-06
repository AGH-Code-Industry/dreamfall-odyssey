using UnityEngine;

public class EnvDamageScript : MonoBehaviour
{
    private PlayerDeathScript deathScript;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerController");
        deathScript = player.GetComponent<PlayerDeathScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deathScript.KillPlayer();
        }
    }
}
