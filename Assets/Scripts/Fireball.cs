using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float baseSpeed;
    public LayerMask enemyLayer;
    List<int> hitIndices = new List<int>();
    Vector3 direction;
    private void Update()
    {
        transform.position += direction * baseSpeed * Time.deltaTime;
        Collider[] hits = Physics.OverlapSphere(transform.position, transform.localScale.x / 2, enemyLayer);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].TryGetComponent(out RobotHealth enemy)
                && !hitIndices.Contains(enemy.index))
            {
                enemy.TakeDamage(1,transform.position,1);
                hitIndices.Add(enemy.index);
            }
        }
    }
    public void Initialize(Vector3 direction)
    {
        this.direction = direction;
       // transform.localScale = new Vector3(fireballInfo.size, fireballInfo.size, fireballInfo.size);
       // Destroy(gameObject, 10);
    }
}
