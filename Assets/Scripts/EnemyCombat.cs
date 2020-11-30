using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyCombat : MonoBehaviour
{
    public float maxHealth;
    public float attackRange;
    public float attackCooldown;
    public float damage;
    public Animator animator;
    internal int index;
    private bool isAttacking;
    private float attackRangeSquared;
    private Healthbar healthbar;
    private Material material;
    private OnDeath onDeath;
    private float curHealth;
    Coroutine takeDamageFlashCoroutine;
    private void Awake()
    {
        attackRangeSquared = attackRange * attackRange;
        material = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        healthbar = GetComponent<Healthbar>();
        onDeath = GetComponent<OnDeath>();
        curHealth = maxHealth;
    }
    public void UpdateFunc()
    {
        if (!isAttacking && IsInAttackRange())
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
        }
        yield return new WaitForSeconds(attackCooldown);

        if (IsInAttackRange())
        {
            GameManager.Instance.player.playerCombat.TakeDamage(damage);
            StartCoroutine(Attack());
        }
        else
        {
            animator.SetBool("isAttacking", false);
            isAttacking = false;
        }
    }
    bool IsInAttackRange()
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        float x1 = transform.position.x;
        float x2 = playerPos.x;
        float y1 = transform.position.y;
        float y2 = playerPos.y;
        float z1 = transform.position.z;
        float z2 = playerPos.z;
        float distanceToPlayerSquared = GameManager.Instance.GetSquaredDistanceBetweenTwoPointsXYZ(x1, x2, y1, y2, z1, z2);
        return distanceToPlayerSquared <= attackRangeSquared;
    }
    /// <summary>
    /// Adds knockback on death
    /// </summary>
    public void TakeDamage(SpellInfo spellInfo)
    {
        curHealth -= spellInfo.damage;
        healthbar.UpdateHealthBar(maxHealth, curHealth);
        if (curHealth <= 0)
        {
            onDeath.Play(spellInfo);

        }
        else
        {
            if (takeDamageFlashCoroutine != null)
            {
                StopCoroutine(takeDamageFlashCoroutine);
            }
            takeDamageFlashCoroutine = StartCoroutine(TakeDamageFlash());
        }
    }

    IEnumerator TakeDamageFlash()
    {
        float lerpValue = 0;
        float lerpSpeed = 7;
        while (material.color != Color.red)
        {
            lerpValue += lerpSpeed * Time.deltaTime;
            material.color = Color.Lerp(Color.white, Color.red, lerpValue);
            yield return new WaitForEndOfFrame();
        }
        lerpValue = 0;
        while (material.color != Color.white)
        {
            lerpValue += lerpSpeed * Time.deltaTime;
            material.color = Color.Lerp(Color.red, Color.white, lerpValue);
            yield return new WaitForEndOfFrame();
        }
    }
    public bool IsAlive()
    {
        return curHealth > 0;
    }
    public void ResetHealth()
    {
        curHealth = maxHealth;
    }
}
