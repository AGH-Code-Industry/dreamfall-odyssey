using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float speed;
    public Transform[] patrol_points;
    public int patrol_destination;

    private List<Transform> playersOnPlatform = new List<Transform>();

    void Update()
    {
        if (patrol_destination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrol_points[0].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrol_points[0].position) < .2f)
            {
                patrol_destination = 1;
            }
        }

        if (patrol_destination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrol_points[1].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrol_points[1].position) < .2f)
            {
                patrol_destination = 0;
            }
        }

        foreach (Transform player in playersOnPlatform)
        {
            if (player != null) 
            {
                player.position = new Vector2(player.position.x + (transform.position.x - player.position.x), player.position.y);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playersOnPlatform.Contains(collision.transform))
            {
                playersOnPlatform.Add(collision.transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playersOnPlatform.Remove(collision.transform);
        }
    }
}
