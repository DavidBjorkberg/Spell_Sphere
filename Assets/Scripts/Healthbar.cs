using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public RectTransform healthBar;
    public Canvas healthBarCanvas;
    public float showHealthBarTime;
    private float showHealthBarTimer;
    Coroutine showHealthbarCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        healthBarCanvas.enabled = false;
    }
    public void UpdateHealthBar(float maxHealth, float curHealth)
    {
        healthBar.sizeDelta = new Vector2((curHealth / maxHealth) * 100, healthBar.sizeDelta.y);
        if (healthBarCanvas.enabled)
        {
            showHealthBarTimer = 0;
        }
        else
        {
            if (gameObject.activeSelf)
            {
                //showHealthbarCoroutine = StartCoroutine(ShowHealthbar());
            }
        }
    }
    IEnumerator ShowHealthbar()
    {
        healthBarCanvas.enabled = true;
        while (showHealthBarTimer < showHealthBarTime)
        {
            showHealthBarTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        showHealthBarTimer = 0;
        healthBarCanvas.enabled = false;
    }
    public void ForceHideHealthbar()
    {
        //StopCoroutine(showHealthbarCoroutine);
        healthBarCanvas.enabled = false;
    }
}
