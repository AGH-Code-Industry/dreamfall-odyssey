using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ScoreScript : MonoBehaviour
{
    public TMP_Text MyscoreText; 
    private int ScoreNumber;

    void Start()
    {
        ScoreNumber = 0;
        MyscoreText.text = "Score: " + ScoreNumber;
    }

    private void OnTriggerEnter2D(Collider2D point)
    {
        Debug.Log("Kolizja z obiektem: " + point.name);
        if (point.tag == "Point")
        {
            
            ScoreNumber++;
            Destroy(point.gameObject); 
            MyscoreText.text = "Score: " + ScoreNumber;
        }
    }
}