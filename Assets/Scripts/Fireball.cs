using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float baseSpeed;
    FireballInfo fireballInfo;
    List<int> hitIndices = new List<int>();
    private void Update()
    {
        transform.position += fireballInfo.direction * baseSpeed * Time.deltaTime;
    }
    public void Initialize(FireballInfo fireballInfo)
    {
        this.fireballInfo = fireballInfo;
        transform.localScale = new Vector3(fireballInfo.size, fireballInfo.size, fireballInfo.size);
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider collision)
    {
        EnemyCombat enemy = collision.GetComponent<EnemyCombat>();
        if (enemy != null
            && !hitIndices.Contains(enemy.index))
        {
            fireballInfo.posWhenHit = transform.position;
            enemy.TakeDamage(fireballInfo);
            hitIndices.Add(enemy.index);
        }
    }
}
