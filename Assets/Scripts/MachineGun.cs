using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TearEffect/MachineGun")]

public class MachineGun : TearEffect
{
    public override string GetName()
    {
        return "MachineGun";
    }
    public override void OnInitialize(Tear tear)
    {
        tear.baseSpeed *= 1.4f;
        tear.transform.localScale = Vector3.one * 0.15f;
        tear.transform.position += Random.insideUnitSphere / 2;
    }
}
