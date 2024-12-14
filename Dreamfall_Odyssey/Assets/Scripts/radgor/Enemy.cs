using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed;
    public bool flipIsActive = true;

    public Vector3 target;

    void Start()
    {
        target = pointA.position;
    }

    void Update()
    {
        
    }

    public void Patrol()
    {
        Debug.Log(target);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (flipIsActive)
        {
            FlipTowards(target);
        }
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
            // transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    protected void FlipTowards(Vector3 targetPosition)
    {
        // Debug.Log("Grzyb: "+transform.position);
        // Debug.Log("Target: " + targetPosition);
        // Obraca przeciwnika w zale¿noœci od kierunku
        if (targetPosition.x > transform.position.x)
        {
            // Ruch w prawo
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y); // Skaluj w prawo
            Debug.Log("PRAWO");
        }
        if (targetPosition.x < transform.position.x)
        {
            // Ruch w lewo
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y); // Skaluj w lewo
            Debug.Log("LEWO");
        }
    }
}
