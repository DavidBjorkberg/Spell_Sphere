using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class TearEffect : ScriptableObject
{
    public virtual void OnInitialize(Tear tear)
    {

    }
    public virtual void OnMove(Tear tear)
    {

    }
    public virtual bool OnHit(Tear tear)
    {
        return true;
    }
    public abstract string GetName();
}
