using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RobotMovement : MonoBehaviour
{
    internal NavMeshAgent navMeshAgent;
    private Robot robot;
    private void Awake()
    {
        robot = GetComponent<Robot>();
        if (!robot.isOnThreat)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }

    public void Initialize(Vector3 walkTarget)
    {
        navMeshAgent.SetDestination(walkTarget);
    }
}
