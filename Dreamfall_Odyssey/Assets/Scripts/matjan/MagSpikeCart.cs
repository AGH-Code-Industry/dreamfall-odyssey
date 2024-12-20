using UnityEngine;
using UnityEngine.SceneManagement;

public class MagSpikeCart : Enemy
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Player plr = col.GetComponent<Player>();
            plr.Die();
        }
    }
}
