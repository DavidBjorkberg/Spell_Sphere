using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RobotMovement : MonoBehaviour
{
    internal NavMeshAgent navMeshAgent;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        navMeshAgent.SetDestination(GameManager.Instance.player.transform.position);
    }
}
