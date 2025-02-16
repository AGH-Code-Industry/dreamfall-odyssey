using UnityEngine;

public class MArrowThrower : MonoBehaviour
{
    public GameObject arrowPrefab;  // Prefab strza?ki
    public Transform firePoint;     // Miejsce, z którego strza?y b?d? wyrzucane
    public float fireRate = 2f;     // Czas pomi?dzy kolejnymi wystrza?ami
    public bool targetOnRightSide = true; // Change to false if you want it to shoot to the left
    public float arrowSpeed = 10f;  // Szybko?? strza?ek
    private float nextFireTime;     // Czas, kiedy mo?na wystrzeli? kolejn? strza??

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
        if (rb != null && targetOnRightSide)
        {
            rb.linearVelocity = firePoint.right * arrowSpeed;
        } else if (rb != null && !targetOnRightSide)
        {
            rb.linearVelocity = -firePoint.right * arrowSpeed;
        }

            Destroy(arrow, 5f);
    }
}