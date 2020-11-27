using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyHandler enemyHandler;
    public GridHandler gridHandler;
    public EnemyGroup enemyGroupPrefab;
    public Formation standardFormation;
    int currentNrOfEnemies = 0;
    const int nrOfStandardGroupsToSpawn = 75; //Define more of these variables to define how many of other kinds of units to spawn
    const int nrOfPartitions =5;
    const int chanceToSpawnInPercent = 30;
    private void Start()
    {
        int nrOfSpawnedGroups = 0;
        int roll;
        int gridSize = gridHandler.nrOfCellsInHeight * gridHandler.nrOfCellsInWidth;
        int partitionSize = gridSize / nrOfPartitions;
        int nrOfGroupsToSpawnPerPartition = nrOfStandardGroupsToSpawn / nrOfPartitions;
        for (int i = 0; i < nrOfPartitions; i++)
        {
            for (int j = 0; j < partitionSize && nrOfSpawnedGroups < nrOfGroupsToSpawnPerPartition; j++)
            {
                roll = Random.Range(0, 100);
                if (roll >= chanceToSpawnInPercent && !gridHandler.GetCell(j + i * partitionSize).isOccupied)
                {
                    currentNrOfEnemies += SpawnGroup(j + i * partitionSize);
                    nrOfSpawnedGroups++;
                }
            }
            nrOfSpawnedGroups = 0;
        }
    }
    int SpawnGroup(int cellIndex)
    {
        EnemyGroup instantiatedGroup = Instantiate(enemyGroupPrefab, Vector3.zero, Quaternion.identity);
        int nrOfCreatedEnemies = instantiatedGroup.Spawn(gridHandler.GetCellPos(cellIndex)
            , currentNrOfEnemies, standardFormation, cellIndex);
        enemyHandler.distantGroups.Add(instantiatedGroup);
        return nrOfCreatedEnemies;
    }
}
