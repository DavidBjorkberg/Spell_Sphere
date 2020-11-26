using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellInfo : ScriptableObject
{
    public float damage;

    public abstract string GetSpellName();
}
