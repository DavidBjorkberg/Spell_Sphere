using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    public Rigidbody ribsRB;
    public Rigidbody[] ragdollRB;
    public BoxCollider baseCollider;
    public Collider[] ragdollColliders;
    private Robot enemy;
    private Healthbar healthbar;
    private float resetTimer = 3;

    private void Awake()
    {
        enemy = GetComponent<Robot>();
        healthbar = GetComponent<Healthbar>();
    }
    public void Play(Vector3 hitPos,float explodePower)
    {
        SwitchColliders();
        ApplyKnockback(hitPos,explodePower);
        GameManager.Instance.AddScore(100);
        enemy.enemyMovement.navMeshAgent.enabled = false;
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
    void ApplyKnockback(Vector3 hitPos,float explodePower)
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
        Destroy(gameObject);
    }
}
