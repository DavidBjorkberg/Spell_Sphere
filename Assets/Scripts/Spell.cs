using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    public string type;
    public int minDamage;
    public int maxDamage;
    public int minDualDamage;
    public int maxDualDamage;

    public string GetSpellType()
    {
        return type;
    }
    public abstract void Cast(Vector3 pos, Vector3 dir, float charge);
    public abstract void DualCast(Vector3 pos, Vector3 dir, float charge);

    protected float GetSpellDamage(float charge)
    {
        return Mathf.Lerp(minDamage, maxDamage, charge);
    }
    protected float GetDualSpellDamage(float charge)
    {
        return Mathf.Lerp(minDualDamage, maxDualDamage, charge);
    }

}
