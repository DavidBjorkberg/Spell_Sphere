using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RobotHealth : MonoBehaviour
{
    public float maxHealth;
    public Animator animator;
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

    public void TakeDamage(float amount, Vector3 hitPos,float explodePower)
    {
        curHealth -= amount;
        healthbar.UpdateHealthBar(maxHealth, curHealth);
        if (curHealth <= 0)
        {
            onDeath.Play(hitPos, explodePower);
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
}
