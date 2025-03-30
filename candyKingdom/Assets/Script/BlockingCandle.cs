using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockingCandle : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;
    [SerializeField] private int requiredDeadEnemies = 2;

    [SerializeField] private Rigidbody2D body;

    private void Update()
    {
        if (EnemyManager.deadEnemyCount >= requiredDeadEnemies)
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        body.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}

