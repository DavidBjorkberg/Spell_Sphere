﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    public Rigidbody ribsRB;
    public Rigidbody[] ragdollRB;
    public BoxCollider baseCollider;
    public Collider[] ragdollColliders;

    private Robot robot;
    private Healthbar healthbar;
    private float resetTimer = 3;

    private void Awake()
    {
        robot = GetComponent<Robot>();
        healthbar = GetComponent<Healthbar>();
    }
    public void Play(Vector3 hitPos, float explodePower)
    {
        SwitchColliders();
        ApplyKnockback(hitPos, explodePower);
        if (robot.isOnThreat)
        {
            robot.threat.PartDied();
            if (robot.threat.GetComponent<Construct>().constructionComplete)
            {
                transform.parent = null;
            }
        }
        else
        {
            robot.spawner.RobotDied();
            robot.robotMovement.navMeshAgent.enabled = false;
        }

        GameManager.Instance.AddScore(100);
        healthbar.ForceHideHealthbar();
        Invoke("ResetEnemy", resetTimer);
    }
    void SwitchColliders()
    {
        for (int i = 0; i < ragdollColliders.Length; i++)
        {
            ragdollColliders[i].enabled = true;
        }
        baseCollider.enabled = false;
    }
    void ApplyKnockback(Vector3 hitPos, float explodePower)
    {
        EnableRagDoll();
        ribsRB.AddExplosionForce(explodePower, hitPos, 5, 1, ForceMode.Impulse);
    }
    void EnableRagDoll()
    {
        foreach (Rigidbody rb in ragdollRB)
        {
            rb.isKinematic = false;
        }
    }
    void ResetEnemy()
    {
        gameObject.SetActive(false);
    }
}
