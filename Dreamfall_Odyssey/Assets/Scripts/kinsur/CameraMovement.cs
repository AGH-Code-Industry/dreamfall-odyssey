using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Kamera pod��a za celem zar�wno w osi X, jak i Y
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
    }
}
