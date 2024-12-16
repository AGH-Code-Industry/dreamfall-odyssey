using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        if (target == null) return;
        transform.position = target.position + new Vector3(0, 0, -10);
    }
}
