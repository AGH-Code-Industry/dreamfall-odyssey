using UnityEngine;
using UnityEngine.UI;

public class HallucinationManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] barImages;
    [SerializeField]
    private Image panel;
    public int amount = 0;
    private BoxCollider2D playerCollider;
    [SerializeField]
    private GameObject hallucinationEnemy;
    private bool spawnActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnHallucinationEnemy();

        if (Input.GetKeyDown(KeyCode.P))
        {
            EatMuschroom();
        }

        if (amount == 0)
        {
            panel.sprite = barImages[0];
        }
        else if (amount == 1){
            panel.sprite = barImages[1];
        }
        else if (amount == 2)
        {
            panel.sprite = barImages[2];
        }
        else if (amount == 3)
        {
            panel.sprite = barImages[3];
        }
        else if (amount == 4)
        {
            panel.sprite = barImages[4];
        }
    }

    private void EatMuschroom()
    {
        amount += 1;
        //amount = Mathf.Clamp(amount, 0, 5);
    }

    private void SpawnHallucinationEnemy()
    {
        if (amount > 2 && spawnActive)
        {
            GameObject newEnemy = Instantiate(hallucinationEnemy, new Vector3(52f, -5f, 0f), Quaternion.identity);
            newEnemy.SetActive(true);
            spawnActive = false;
        }
    }
}
