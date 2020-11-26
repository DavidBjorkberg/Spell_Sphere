using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    public float chaseDistance;
    internal bool chasing;
    internal NavMeshAgent navMeshAgent;
    private EnemyCombat enemyCombat;
    private void Awake()
    {
        enemyCombat = GetComponent<EnemyCombat>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (chasing)
        {
            Vector3 playerPos = GameManager.Instance.player.transform.position;
            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 randomPointAroundPlayer = playerPos + new Vector3(randomPoint.x, 0, randomPoint.y);
            navMeshAgent.SetDestination(playerPos);
            float distance = (playerPos - transform.position).magnitude;
            if (distance <= chaseDistance)
            {
                if (distance <= navMeshAgent.stoppingDistance)
                {
                    FaceTarget(playerPos);
                }
            }
        }



    }
    void FaceTarget(Vector3 playerPos)
    {
        Vector3 direction = (playerPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
