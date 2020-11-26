using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballInfo : SpellInfo
{
    public float size;
    public float knockbackStrength;
    public float upwardsModifier;
    public Vector3 direction;
    public Vector3 posWhenHit;

    public override string GetSpellName()
    {
        return "Fireball";
    }
}
