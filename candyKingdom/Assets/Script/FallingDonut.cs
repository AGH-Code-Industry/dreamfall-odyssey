using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDonut : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;

    private float shakeDuration = 0.1f; 
    private float shakeMagnitude = 0.04f;

    [SerializeField] private Rigidbody2D body;
    private Vector3 originalPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        originalPosition = transform.position;

        float shakeTime = 0f;
        while (shakeTime < shakeDuration)
        {
            float shakeX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float shakeY = Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = originalPosition + new Vector3(shakeX, shakeY, 0);

            shakeTime += Time.deltaTime;
            yield return null; 
        }

        yield return new WaitForSeconds(fallDelay);
        body.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}

