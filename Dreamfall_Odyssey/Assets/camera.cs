using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float camSpeed = 3f;
    public float cam_offset = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (player.position.x - transform.position.x)*Time.deltaTime * camSpeed,transform.position.y + (player.position.y + cam_offset - transform.position.y)*Time.deltaTime * camSpeed/3f, 0f);
    }
}
