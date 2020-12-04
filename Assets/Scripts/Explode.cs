using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : TearEffect
{
    public float explodeRadius = 5;
    public override bool OnHit(Tear tear)
    {
        Collider[] hits = Physics.OverlapSphere(tear.transform.position, explodeRadius, 1 << 8);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RobotHealth enemy = hits[i].GetComponent<RobotHealth>();
                enemy.TakeDamage(1, tear.transform.position, 1000);
            }
            Destroy(tear.gameObject);
            return false;
        }
        return true;
    }
}
