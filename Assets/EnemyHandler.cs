using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    internal List<EnemyGroup> distantGroups = new List<EnemyGroup>();
    internal List<EnemyGroup> closeGroups = new List<EnemyGroup>();
    public GridHandler gridHandler;
    private void Update()
    {
        UpdateCloseGroups();
        UpdateDistantGroups();

    }
    void UpdateCloseGroups()
    {
        if (closeGroups.Count > 0)
        {
            AssignToFreeAttackSpots();
        }
        for (int i = 0; i < closeGroups.Count; i++)
        {
            if (closeGroups[i] == null)
            {
                closeGroups.RemoveAt(i);
                int freeCell = gridHandler.GetFreeCellIndex();
                GetComponent<EnemySpawner>().SpawnGroup(freeCell);
                continue;
            }
            closeGroups[i].CloseGroupUpdate();
        }
    }
    void AssignToFreeAttackSpots()
    {
        int freeSpotIndex;
        freeSpotIndex = GameManager.Instance.player.attackSpots.GetFreeSpot();
        if (freeSpotIndex != -1)
        {
            Vector3 freeSpotPos = GameManager.Instance.player.attackSpots.GetSpotPos(freeSpotIndex);
            Enemy closestEnemy = GetClosestRoamingEnemy(freeSpotPos);
            while (freeSpotIndex != -1 && closestEnemy != null)
            {
                GameManager.Instance.player.attackSpots.ClaimSpot(freeSpotIndex);
                closestEnemy.enemyMovement.AssignAttackSpot(freeSpotIndex);

                freeSpotPos = GameManager.Instance.player.attackSpots.GetSpotPos(freeSpotIndex);
                closestEnemy = GetClosestRoamingEnemy(freeSpotPos);
                freeSpotIndex = GameManager.Instance.player.attackSpots.GetFreeSpot();
            }
        }
    }
    Enemy GetClosestRoamingEnemy(Vector3 pos)
    {
        int closestEnemyGrpIndex = -1;
        int closestEnemyIndex = -1;
        float closestDistance = -1;
        float distance;
        float x1;
        float x2;
        float z1;
        float z2;
        for (int i = 0; i < closeGroups.Count; i++)
        {
            for (int j = 0; j < closeGroups[i].enemies.Count; j++)
            {
                if (closeGroups[i].enemies[j].enemyMovement.spotTakenIndex == -1)
                {
                    x1 = closeGroups[i].enemies[j].transform.position.x;
                    x2 = pos.x;
                    z1 = closeGroups[i].enemies[j].transform.position.z;
                    z2 = pos.z;
                    distance = GameManager.Instance.GetCheapDistanceBetweenTwoPointsXZ(x1, x2, z1, z2);
                    if (distance < closestDistance || closestDistance == -1)
                    {
                        closestDistance = distance;
                        closestEnemyGrpIndex = i;
                        closestEnemyIndex = j;
                    }
                }
            }
        }
        if (closestEnemyGrpIndex != -1)
        {
            return closeGroups[closestEnemyGrpIndex].enemies[closestEnemyIndex];
        }
        else
        {
            return null;
        }
    }
    void UpdateDistantGroups()
    {
        for (int i = distantGroups.Count - 1; i >= 0; i--)
        {
            if (distantGroups[i] == null)
            {
                distantGroups.RemoveAt(i);
                continue;
            }
            if (!distantGroups[i].isChasing)
            {
                if (distantGroups[i].IsInChaseRange())
                {
                    distantGroups[i].StartChasing();
                    closeGroups.Add(distantGroups[i]);
                    distantGroups.RemoveAt(i);
                }
                else
                {
                    HandleGroupMovement(i);
                }
            }
        }
    }
    void HandleGroupMovement(int groupIndex)
    {

        if (!distantGroups[groupIndex].HasPath() && !distantGroups[groupIndex].isChasing)
        {
            int[] neighbours = gridHandler.GetCellNeighbours(distantGroups[groupIndex].currentCellIndex);
            distantGroups[groupIndex].AlignToPlayerDirection(neighbours);
            int nextCellIndex = distantGroups[groupIndex].CalculateNextCellIndex(neighbours);
            if (nextCellIndex != -1)
            {
                distantGroups[groupIndex].MoveToCell(gridHandler.GetCellPos(nextCellIndex), nextCellIndex);
            }
        }
    }

}
