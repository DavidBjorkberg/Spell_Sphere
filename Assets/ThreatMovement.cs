using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThreatMovement : MonoBehaviour
{
    NavMeshAgent navmeshAgent;
    Construct construct;
    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        navmeshAgent.enabled = false;
        construct = GetComponent<Construct>();
    }
    private void Update()
    {
        if (construct.constructionComplete)
        {
            navmeshAgent.enabled = true;
            navmeshAgent.SetDestination(GameManager.Instance.player.transform.position);
        }
    }
}
