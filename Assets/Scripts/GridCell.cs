using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : ScriptableObject
{
    public int index;
    public Vector3 pos;
    internal bool isOccupied = false; 
    public void Initialize(int index, Vector3 pos)
    {
        this.index = index;
        this.pos = pos;
    }
    
}
