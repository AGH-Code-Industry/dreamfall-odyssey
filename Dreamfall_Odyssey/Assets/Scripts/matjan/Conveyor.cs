using UnityEngine;

public class Conveyor : MonoBehaviour
{

    public float move_force = 0.01f;
    private bool colliding_with_player = false;
    private Player plr = null;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            colliding_with_player = true;
            plr = col.GetComponent<Player>();
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            colliding_with_player = false;
        }
    }

    private void FixedUpdate()
    {
        if (colliding_with_player && plr != null && plr.IsGrounded())
        {
            plr.extra_vel = new Vector2(move_force, 0f);
        }
    }
}
