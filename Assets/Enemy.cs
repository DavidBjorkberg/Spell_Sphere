using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyCombat enemyCombat;
    public EnemyMovement enemyMovement;
    public OnDeath onDeath;
    public bool isActive;
    private void Awake()
    {
        isActive = true;
    }
    public void CloseEnemyUpdate()
    {
        enemyMovement.UpdateFunc();
        enemyCombat.UpdateFunc();
    }
}
