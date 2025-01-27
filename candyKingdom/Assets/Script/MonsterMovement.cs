using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    public Transform[] patrol_points;
    public int patrol_destination;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    private bool isStopped = false;

    void Update()
    {
        if (isStopped) return;

        if (isChasing)
        {
            if(transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(-3, 3, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(3, 3, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if(Vector2.Distance(transform.position, playerTransform.position) > chaseDistance + 5)
            {
                isChasing = false;
            }
        }
        else
        {
            if(Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }

            if (patrol_destination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrol_points[0].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrol_points[0].position) < .2f)
                {
                    transform.localScale = new Vector3(3, 3, 1);
                    patrol_destination = 1;
                }
            }

            if (patrol_destination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrol_points[1].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrol_points[1].position) < .2f)
                {
                    transform.localScale = new Vector3(-3, 3, 1);
                    patrol_destination = 0;
                }
            }
        }
    }

    public IEnumerator StopMovement(float duration)
    {
        isStopped = true;
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }
}
