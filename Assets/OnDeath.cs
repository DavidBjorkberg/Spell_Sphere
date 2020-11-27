using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    public Rigidbody ribsRB;
    public Rigidbody[] ragdollRB;
    public BoxCollider baseCollider;
    public Collider[] ragdollColliders;
    internal EnemyGroup group;
    private Enemy enemy;
    private Healthbar healthbar;
    private float resetTimer = 3;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        healthbar = GetComponent<Healthbar>();
    }
    public void Play(SpellInfo spellInfo)
    {
        SwitchColliders();
        CallSpellEffect(spellInfo);

        enemy.enemyMovement.LeaveAttackSpot();
        enemy.enemyMovement.navMeshAgent.enabled = false;
        enemy.isActive = false;
        group.RemoveEnemyFromGroup(enemy);
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
    void CallSpellEffect(SpellInfo spellInfo)
    {
        string spellName = spellInfo.GetSpellName();
        if (string.Compare(spellName, "Fireball", System.StringComparison.Ordinal) == 0)
        {
            FireballInfo fireballInfo = spellInfo as FireballInfo;
            ApplyKnockback(fireballInfo);
        }
        else if (string.Compare(spellName, "ChainLightning", System.StringComparison.Ordinal) == 0)
        {

        }
    }
    void ApplyKnockback(FireballInfo fireballInfo)
    {
        EnableRagDoll();
        ribsRB.AddExplosionForce(fireballInfo.knockbackStrength, fireballInfo.posWhenHit, fireballInfo.size, fireballInfo.upwardsModifier, ForceMode.Impulse);
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
        for (int i = 0; i < ragdollColliders.Length; i++)
        {
            ragdollColliders[i].enabled = false;
        }
        for (int i = 0; i < ragdollRB.Length; i++)
        {
            ragdollRB[i].isKinematic = true;
        }
        baseCollider.enabled = true;
        enemy.enemyCombat.ResetHealth();
        transform.position = new Vector3(0, -5, 0);
    }
}
