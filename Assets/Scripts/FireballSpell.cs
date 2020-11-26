using UnityEngine;
[CreateAssetMenu(menuName = "Spell/Fireball")]
public class FireballSpell : Spell
{
    FireballInfo fireballInfo;
    public Fireball prefab;
    public float minSize;
    public float maxSize;
    public float minDualSize;
    public float maxDualSize;
    public float minKnockback;
    public float maxKnockback;
    public float minDualKnockback;
    public float maxDualKnockback;
    public float upwardsModifier;
    public override void Cast(Vector3 pos, Vector3 dir, float charge)
    {
        Fireball instantiatedFireball = Instantiate(prefab, pos, Quaternion.identity);
        fireballInfo = CreateInstance("FireballInfo") as FireballInfo;
        fireballInfo.damage = GetSpellDamage(charge);
        fireballInfo.size = GetSize(charge);
        fireballInfo.knockbackStrength = GetKnockbackStrength(charge);
        fireballInfo.direction = dir;
        fireballInfo.upwardsModifier = upwardsModifier;
              
        instantiatedFireball.Initialize(fireballInfo);
    }
    public override void DualCast(Vector3 pos, Vector3 dir, float charge)
    {
        Fireball instantiatedFireball = Instantiate(prefab, pos, Quaternion.identity);
        fireballInfo = CreateInstance("FireballInfo") as FireballInfo;

        fireballInfo.damage = GetDualSpellDamage(charge);
        fireballInfo.size = GetDualSize(charge);
        fireballInfo.knockbackStrength = GetDualKnockbackStrength(charge);
        fireballInfo.direction = dir;
        fireballInfo.upwardsModifier = upwardsModifier;

        instantiatedFireball.Initialize(fireballInfo);
    }
    float GetKnockbackStrength(float charge)
    {
        return Mathf.Lerp(minKnockback, maxKnockback, charge);
    }
    float GetDualKnockbackStrength(float charge)
    {
        return Mathf.Lerp(minDualKnockback, maxDualKnockback, charge);
    }
    float GetSize(float charge)
    {
        return Mathf.Lerp(minSize, maxSize, charge);
    }
    float GetDualSize(float charge)
    {
        return Mathf.Lerp(minDualSize, maxDualSize, charge);
    }
}
