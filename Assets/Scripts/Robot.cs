using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Robot : MonoBehaviour
{
    public Animator animator;
    public ThreatHealth threat;
    public SpawnRobots spawner;
    public bool isOnThreat;
    private RobotHealth robotHealth;
    internal RobotMovement robotMovement;
    private OnDeath onDeath;
    private void Awake()
    {
        robotHealth = GetComponent<RobotHealth>();
        robotMovement = GetComponent<RobotMovement>();
        onDeath = GetComponent<OnDeath>();
        if (isOnThreat)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        animator.SetBool("isActive", !isOnThreat);
    }
    public void Initialize(Vector3 constructionPos)
    {
        robotMovement.Initialize(constructionPos);
    }

}
