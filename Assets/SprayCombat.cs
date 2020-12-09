using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCombat : MonoBehaviour
{
    public float attackSpeed;
    public float damage;
    public float explodePower;
    public LayerMask enemyLayer;
    public Camera fpsCamera;
    public float shotAngle;
    public float range;
    private float attackTimer;

    void Start()
    {

    }
    void Update()
    {
        attackTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && attackTimer < 0)
        {
            Vector3 randomDir = RandomCircleDir();
            if (Physics.Raycast(fpsCamera.transform.position, randomDir, out RaycastHit hit, 100, enemyLayer))
            {
                hit.collider.GetComponent<RobotHealth>().TakeDamage(damage, fpsCamera.transform.forward, explodePower);
            }
            Debug.DrawLine(fpsCamera.transform.position, randomDir * range);
            attackTimer = attackSpeed;

        }
    }
    Vector3 RandomCircleDir()
    {
        float radius = Mathf.Tan(Mathf.Deg2Rad * shotAngle / 2) * range;
        Vector2 circle = Random.insideUnitCircle * radius;

        return fpsCamera.transform.forward * range + fpsCamera.transform.rotation * new Vector3(circle.x, circle.y);
    }
}
