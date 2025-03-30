using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int deadEnemyCount = 0;

    public static void RegisterEnemyDeath()
    {
        deadEnemyCount++;
    }
}