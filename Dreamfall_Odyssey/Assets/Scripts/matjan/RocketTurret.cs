using System.Threading;
using UnityEngine;

public class RocketTurret : MonoBehaviour
{
    private float rocket_timer = 0f;

    public float rocket_delay = 3f;
    public float rocket_speed = 4f;
    public Rocket rocket;
    public bool flipped = false;
    public Transform sprite;

    private void Start()
    {
        if (flipped)
        {
            sprite.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (rocket_timer <= 0) {
            Shoot();
        }
        rocket_timer -= Time.deltaTime;
    }

    void Shoot()
    {
        rocket_timer = rocket_delay;
        Rocket proj = Instantiate(rocket, transform.position, Quaternion.identity);
        if (flipped) proj.facing = -1;
        proj.speed = rocket_speed;
    }
}
