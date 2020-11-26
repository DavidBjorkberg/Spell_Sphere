using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyCombat : MonoBehaviour
{

    public float maxHealth;
    public float attackRange;
    internal int index;
    private Healthbar healthbar;
    private Material material;
    private OnDeath onDeath;
    private float curHealth;
    Coroutine takeDamageFlashCoroutine;
    private void Awake()
    {
        material = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        healthbar = GetComponent<Healthbar>();
        onDeath = GetComponent<OnDeath>();
        curHealth = maxHealth;
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
