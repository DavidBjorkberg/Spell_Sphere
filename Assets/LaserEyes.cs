using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEyes : MonoBehaviour
{
    public Transform eyePos;
    public EyeLaser eyeLaserPrefab;

    public float damage;
    public float speed;
    public float cooldown;
    public float range;
    private float cooldownTimer;
    Construct construct;

    private void Awake()
    {
        construct = GetComponent<Construct>();
    }
    private void Update()
    {
        if (construct.constructionComplete)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0 && IsInRange()    )
            {
                Shoot();
                cooldownTimer = cooldown;
            }
        }
    }

    void Shoot()
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 direction = (playerPos - eyePos.position).normalized;

        EyeLaser eyeLaserGO = Instantiate(eyeLaserPrefab, eyePos.position, Quaternion.identity);
        eyeLaserGO.Initialize(direction, damage, speed);
    }
    bool IsInRange()
    {
        float deltaX = transform.position.x - GameManager.Instance.player.transform.position.x;
        float deltaZ = transform.position.z - GameManager.Instance.player.transform.position.z;

        float distance = (deltaX * deltaX) + (deltaZ * deltaZ);
        return distance <= (range * range);
    }
}
