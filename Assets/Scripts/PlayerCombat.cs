using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerCombat : MonoBehaviour
{
    public GameObject rightArm;
    public GameObject leftArm;
    public Camera playerCamera;
    public Spell leftSpell;
    public Spell rightSpell;
    public float maxHealth;
    public RectTransform healthBar;
    private float curHealth;
    private bool chargingLeft;
    private bool chargingRight;
    private bool chargingDual;
    private float leftCharge;
    private float rightCharge;
    private float dualCharge;
    private float heldLeftButtonTimer;
    private float heldRightButtonTimer;
    private readonly float chargeDelay = 0.2f;

    private void Awake()
    {
        curHealth = maxHealth;

    }
    void Update()
    {
        SpellSequence();
    }
    public void TakeDamage(float amount)
    {
        curHealth -= amount;
        healthBar.sizeDelta = new Vector2(curHealth / maxHealth * 500, healthBar.sizeDelta.y);
        if(curHealth < 0)
        {
           Died();
        }
    }
    void Died()
    {
        SceneManager.LoadScene(0);
    }
    void SpellSequence()
    {
        UpdateHeldButtonTimers();
        StartChargeSpells();
        ChargeSpells();
        ReleaseSpells();
    }
    void StartChargeSpells()
    {
        bool leftMouseDown = Input.GetMouseButton(0);
        bool rightMouseDown = Input.GetMouseButton(1);
        bool rightTimerHigherThanDelay = heldRightButtonTimer >= chargeDelay;
        bool leftTimerHigherThanDelay = heldLeftButtonTimer >= chargeDelay;
        if (!chargingLeft && !chargingRight)
        {
            if (leftMouseDown && rightMouseDown && (rightTimerHigherThanDelay || leftTimerHigherThanDelay))
            {
                chargingDual = true;
            }
        }
        if (!chargingDual)
        {
            if (leftTimerHigherThanDelay)
            {
                chargingLeft = true;
            }
            if (rightTimerHigherThanDelay)
            {
                chargingRight = true;
            }
        }
    }
    void ChargeSpells()
    {
        if (chargingLeft)
        {
            ChargeLeftSpell();
        }
        if (chargingRight)
        {
            ChargeRightSpell();
        }
        if (chargingDual)
        {
            ChargeDualSpell();
        }
    }
    void ReleaseSpells()
    {
        bool leftMouseDown = Input.GetMouseButton(0);
        bool rightMouseDown = Input.GetMouseButton(1);
        if (!leftMouseDown)
        {
            heldLeftButtonTimer = 0;
            if (leftCharge > 0)
            {
                CastLeftSpell();
                chargingLeft = false;
            }
        }
        if (!rightMouseDown)
        {
            heldRightButtonTimer = 0;
            if (rightCharge > 0)
            {
                CastRightSpell();
                chargingRight = false;
            }
        }
        if (dualCharge > 0 && !leftMouseDown && !rightMouseDown)
        {
            CastDualSpell();
            chargingDual = false;
        }
    }
    void UpdateHeldButtonTimers()
    {
        bool leftMouseDown = Input.GetMouseButton(0);
        bool rightMouseDown = Input.GetMouseButton(1);

        if (leftMouseDown)
        {
            heldLeftButtonTimer += Time.deltaTime;
        }
        if (rightMouseDown)
        {
            heldRightButtonTimer += Time.deltaTime;
        }
    }
    void ChargeLeftSpell()
    {
        leftCharge += Time.deltaTime;
        leftCharge = Mathf.Clamp01(leftCharge);
        leftArm.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.green, leftCharge);
    }
    void ChargeRightSpell()
    {
        rightCharge += Time.deltaTime;
        rightCharge = Mathf.Clamp01(rightCharge);
        rightArm.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.green, rightCharge);
    }
    void ChargeDualSpell()
    {
        dualCharge += Time.deltaTime;
        dualCharge = Mathf.Clamp01(dualCharge);
        rightArm.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.red, dualCharge);
        leftArm.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.red, dualCharge);

    }
    void CastLeftSpell()
    {
        leftArm.GetComponent<MeshRenderer>().material.color = Color.white;
        leftSpell.Cast(leftArm.transform.position + playerCamera.transform.forward, playerCamera.transform.forward, leftCharge);

        leftCharge = 0;
    }
    void CastRightSpell()
    {
        rightArm.GetComponent<MeshRenderer>().material.color = Color.white;
        rightSpell.Cast(rightArm.transform.position + playerCamera.transform.forward, playerCamera.transform.forward, rightCharge);

        rightCharge = 0;
    }
    void CastDualSpell()
    {
        leftSpell.DualCast(transform.position + playerCamera.transform.forward, playerCamera.transform.forward, dualCharge);
        leftArm.GetComponent<MeshRenderer>().material.color = Color.white;
        rightArm.GetComponent<MeshRenderer>().material.color = Color.white;
        dualCharge = 0;
    }
}
