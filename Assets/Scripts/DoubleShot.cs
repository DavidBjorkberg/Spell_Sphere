using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TearEffect/DoubleShot")]

public class DoubleShot : TearEffect
{
    public override void OnInitialize(Tear tear)
    {
        Tear extraTear = Instantiate(tear, tear.transform.position, Quaternion.identity);
        extraTear.direction = tear.direction;
        for (int i = 0; i < tear.tearEffects.Count; i++)
        {
            if (tear.tearEffects[i].ToString() == " (DoubleShot)")
            {
                tear.tearEffects.RemoveAt(i);
                break;
            }
        }
        extraTear.tearEffects.Clear();
        extraTear.Initialize(extraTear.direction, tear.tearEffects);

        tear.transform.position += tear.transform.right;
        extraTear.transform.position -= tear.transform.right;
    }
    public override string GetName()
    {
        return "DoubleShot";
    }
}
