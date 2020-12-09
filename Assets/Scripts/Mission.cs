using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Mission/Mission")]
public class Mission : ScriptableObject
{
    public int nrOfEnemiesToKill;
    public TearEffect reward;
}
