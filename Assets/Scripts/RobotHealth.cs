using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RobotHealth : MonoBehaviour
{
    public float maxHealth;
    internal int index;
    private Healthbar healthbar;
    private Material[] materials;
    private OnDeath onDeath;
    private float curHealth;
    Coroutine takeDamageFlashCoroutine;
    private void Awake()
    {
        materials = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials;
        healthbar = GetComponent<Healthbar>();
        onDeath = GetComponent<OnDeath>();
        curHealth = maxHealth;
    }

    public void TakeDamage(float amount, Vector3 hitDir, float explodePower)
    {
        if (gameObject.activeSelf)
        {
            if (curHealth > 0)
            {
                curHealth -= amount;
                healthbar.UpdateHealthBar(maxHealth, curHealth);
                if (curHealth <= 0)
                {
                    onDeath.Play(hitDir, explodePower);
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
        }
    }
    IEnumerator TakeDamageFlash()
    {
        float lerpValue = 0;
        float lerpSpeed = 7;

        while (materials[0].color != Color.red)
        {
            foreach (Material material in materials)
            {
                lerpValue += lerpSpeed * Time.deltaTime;
                material.color = Color.Lerp(Color.white, Color.red, lerpValue);
                yield return new WaitForEndOfFrame();
            }
        }
        lerpValue = 0;
        while (materials[0].color != Color.white)
        {
            foreach (Material material in materials)
            {
                lerpValue += lerpSpeed * Time.deltaTime;
                material.color = Color.Lerp(Color.red, Color.white, lerpValue);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}