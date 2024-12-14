using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : Enemy
{
    public bool isChasing;
    public Transform playerTransform;
    public float chaseDistance;
    public Animator animator;
    public Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        //Debug.Log(rb.linearVelocity.x);
        if (isChasing)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) >= chaseDistance)
            {
                isChasing = false;
            }

            if (transform.position.x > playerTransform.position.x)
            {
                flipIsActive = false;
                transform.position += Vector3.left * (speed+2) * Time.deltaTime;
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            if (transform.position.x < playerTransform.position.x)
            {
                flipIsActive = false;
                transform.position += Vector3.right * (speed+2) * Time.deltaTime;
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }
        else
        {
            flipIsActive = true;
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
            Patrol();
        }
    }
}
