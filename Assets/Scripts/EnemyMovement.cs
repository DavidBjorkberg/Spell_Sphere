using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    public float chaseDistance;
    internal bool insideChaseRange;
    internal NavMeshAgent navMeshAgent;
    internal int spotTakenIndex = -1;
    private EnemyCombat enemyCombat;
    private Enemy enemy;
    private Vector3 targetOffset;
    private float standardRadius = 0.5f;
    private float noCollRadius = 0.07f;
    float minChaseCircle = 2.0f;
    float maxChaseCircle = 5.0f;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemyCombat = GetComponent<EnemyCombat>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (insideChaseRange && enemy.isActive)
        {
            Vector3 playerPos = GameManager.Instance.player.transform.position;
            FaceTarget(playerPos);
            navMeshAgent.SetDestination(playerPos + targetOffset);
        }
    }

    public void EnteredChaseRange()
    {
        navMeshAgent.ResetPath();
        insideChaseRange = true;
        int closestAttackSpotIndex = GameManager.Instance.player.attackSpots.GetAndClaimClosestSpot(transform.position);
        if (closestAttackSpotIndex == -1)
        {
            targetOffset = GetRandomPointAroundPlayer();
        }
        else
        {
            AssignAttackSpot(closestAttackSpotIndex);
        }
    }
    public void AssignAttackSpot(int index)
    {
        spotTakenIndex = index;
        navMeshAgent.radius = noCollRadius;
        targetOffset = GameManager.Instance.player.attackSpots.GetSpotPos(index);
    }
    public void LeaveAttackSpot()
    {
        if (spotTakenIndex != -1)
        {
            GameManager.Instance.player.attackSpots.UnclaimSpot(spotTakenIndex);
        }
    }
    void FaceTarget(Vector3 playerPos)
    {
        Vector3 lookAtPos = new Vector3(playerPos.x, transform.position.y, playerPos.z);
        Vector3 direction = (playerPos - transform.position).normalized;
        transform.LookAt(lookAtPos);
        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    Vector3 GetRandomPointAroundPlayer()
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector2 randomPointUnitSphere = Random.insideUnitCircle;
        float randomPointX = Mathf.Lerp(minChaseCircle, maxChaseCircle, Mathf.Abs(randomPointUnitSphere.x));
        float randomPointZ = Mathf.Lerp(minChaseCircle, maxChaseCircle, Mathf.Abs(randomPointUnitSphere.y));
        if (randomPointUnitSphere.x < 0)
        {
            randomPointX *= -1;
        }
        if (randomPointUnitSphere.y < 0)
        {
            randomPointZ *= -1;
        }
        Vector3 randomPointAroundPlayer = new Vector3(randomPointX, 0, randomPointZ);
        return randomPointAroundPlayer;
    }
}
