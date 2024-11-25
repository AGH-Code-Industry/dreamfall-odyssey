using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody2D body;

   [SerializeField] private float speed;
    [SerializeField] private float jump;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        if(Input.GetKey(KeyCode.Space))
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jump);
        }
    }
}
