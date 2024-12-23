using UnityEngine;

public class arrowThrower : MonoBehaviour
{
    public GameObject arrowPrefab;  // Prefab strzałki
    public Transform firePoint;     // Miejsce, z którego strzały będą wyrzucane
    public float fireRate = 2f;     // Czas pomiędzy kolejnymi wystrzałami
    public float arrowSpeed = 10f;  // Szybkość strzałek
    private float nextFireTime;     // Czas, kiedy można wystrzelić kolejną strzałę

    void Update()
    {
        
        if (Time.time >= nextFireTime)
        {
            FireArrow();
            nextFireTime = Time.time + 1f / fireRate; 
        }
    }

    void FireArrow()
    {
        
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * arrowSpeed;
        }

        Destroy(arrow, 5f);
    }
}