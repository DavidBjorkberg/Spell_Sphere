using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject rightArm;
    public GameObject leftArm;
    public Camera playerCamera;
    public Spell leftSpell;
    public Spell rightSpell;
    bool chargingLeft;
    bool chargingRight;
    bool chargingDual;
    float leftCharge;
    float rightCharge;
    float dualCharge;
    float heldLeftButtonTimer;
    float heldRightButtonTimer;
    readonly float chargeDelay = 0.2f;

    void Update()
    {
        SpellSequence();
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
