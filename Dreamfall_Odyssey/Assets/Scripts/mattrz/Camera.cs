using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        transform.position = target.position + new Vector3(0, 0, -10);
    }
}
