using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLaser : MonoBehaviour
{
    Vector3 direction;
    float damage;
    float speed;

    private void Update()
    {
        Move();
    }
    public void Initialize(Vector3 dir, float damage, float speed)
    {
        this.direction = dir;
        this.damage = damage;
        this.speed = speed;
    }
    void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(transform.position + direction);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerCombat player))
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}
