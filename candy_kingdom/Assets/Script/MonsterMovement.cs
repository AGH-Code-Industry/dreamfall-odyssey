using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    public Transform[] patrol_points;
    public int patrol_destination;

    private void Update()
    {
        if(patrol_destination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrol_points[0].position, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrol_points[0].position) <  .2f)
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
    }

}
