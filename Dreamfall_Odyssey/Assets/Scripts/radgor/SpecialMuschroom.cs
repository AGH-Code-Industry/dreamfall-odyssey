using UnityEngine;

public class SpecialMuschroom : MonoBehaviour
{
    [SerializeField]
    private HallucinationManager hm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hm.amount += 1;
            Debug.Log("Eating Muschroom...");
            Destroy(gameObject);
        }
    }
}
