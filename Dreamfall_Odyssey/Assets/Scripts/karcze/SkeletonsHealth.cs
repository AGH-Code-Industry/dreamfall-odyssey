using UnityEngine;

public class SkeletonsHealth : MonoBehaviour
{
    public float skeletonHealth;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (skeletonHealth <= 0)
        {
            Debug.Log("Enemy is dead");
        }
    }
}
