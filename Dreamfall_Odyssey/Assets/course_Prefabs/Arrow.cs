using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 10;  // Obrażenia, które zadaje strzała
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Możesz dodać dodatkową logikę, np. zadawanie obrażen
     
        // Zniszczenie strzały po kolizji
        Destroy(gameObject);
    }
}

