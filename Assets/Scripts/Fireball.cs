using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float baseSpeed;
    public LayerMask enemyLayer;
    FireballInfo fireballInfo;
    List<int> hitIndices = new List<int>();
    private void Update()
    {
        transform.position += fireballInfo.direction * baseSpeed * Time.deltaTime;
        Collider[] hits = Physics.OverlapSphere(transform.position, fireballInfo.size / 2, enemyLayer);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].TryGetComponent(out EnemyCombat enemy)
                && !hitIndices.Contains(enemy.index))
            {
                fireballInfo.posWhenHit = transform.position;
                enemy.TakeDamage(fireballInfo);
                hitIndices.Add(enemy.index);
            }
        }
    }
    public void Initialize(FireballInfo fireballInfo)
    {
        this.fireballInfo = fireballInfo;
        transform.localScale = new Vector3(fireballInfo.size, fireballInfo.size, fireballInfo.size);
        Destroy(gameObject, 10);
    }
}
