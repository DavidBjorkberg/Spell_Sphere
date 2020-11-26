using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    internal List<EnemyGroup> groups = new List<EnemyGroup>();
    public GridHandler gridHandler;
    private void Update()
    {
        for (int i = groups.Count - 1; i >= 0; i--)
        {
            HandleGroupMovement(i);
        }
    }
    void HandleGroupMovement(int groupIndex)
    {
        if (groups[groupIndex] == null)
        {
            groups.RemoveAt(groupIndex);
        }
        else if (!groups[groupIndex].HasPath() && !groups[groupIndex].isChasing)
        {
            int[] neighbours = gridHandler.GetCellNeighbours(groups[groupIndex].currentCellIndex);
            groups[groupIndex].AlignToPlayerDirection(neighbours);
            int nextCellIndex = groups[groupIndex].CalculateNextCellIndex(neighbours);
            if (nextCellIndex != -1)
            {
                groups[groupIndex].MoveToCell(gridHandler.GetCellPos(nextCellIndex), nextCellIndex);
            }
        }
    }

}
