using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public Enemy enemyPrefab;
    Formation formation;
    internal List<Enemy> enemies = new List<Enemy>();
    internal int currentCellIndex;
    internal Vector3 currentCellPos;
    internal bool isChasing;
    const float destinationReachedThreshold = 1f;
    const float walkLeftWeight = 1;
    const float walkForwardLeftWeight = 1;
    const float walkRightWeight = 1;
    const float walkForwardRightWeight = 1;
    public bool IsInChaseRange()
    {
        float distance = (enemies[0].transform.position - GameManager.Instance.player.transform.position).magnitude;
        return distance < enemies[0].enemyMovement.chaseDistance;
    }
    public void StartChasing()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].enemyMovement.EnteredChaseRange();
            isChasing = true;
        }
    }
    public int Spawn(Vector3 centerOfCell, int currentNrOfEnemies, Formation formation, int cellIndex)
    {
        this.formation = formation;
        GameManager.Instance.gridHandler.GetCell(cellIndex).isOccupied = true;
        currentCellIndex = cellIndex;
        currentCellPos = centerOfCell;
        for (int i = 0; i < formation.position.Length; i++)
        {
            Vector3 spawnPos = centerOfCell + formation.position[i];
            Enemy instantiatedEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            instantiatedEnemy.enemyCombat.index = currentNrOfEnemies + i;
            instantiatedEnemy.onDeath.group = this;
            instantiatedEnemy.transform.parent = transform;
            enemies.Add(instantiatedEnemy);
        }
        return formation.position.Length;
    }
    public void MoveToCell(Vector3 cellPos, int cellIndex)
    {
        GameManager.Instance.gridHandler.GetCell(currentCellIndex).isOccupied = false;
        currentCellIndex = cellIndex;
        GameManager.Instance.gridHandler.GetCell(cellIndex).isOccupied = true;
        currentCellPos = cellPos;
        for (int i = 0; i < enemies.Count; i++)
        {
            Vector3 finalPos = cellPos + formation.position[i];
            enemies[i].enemyMovement.navMeshAgent.SetDestination(finalPos);
        }
    }
    public bool HasPath()
    {
        return enemies[0].enemyMovement.navMeshAgent.remainingDistance >= destinationReachedThreshold;
    }
    public int CalculateNextCellIndex(int[] neighbours)
    {
        int nextCellIndex = -1;
        if (neighbours[0] != -1) //Always go towards player if possible
        {
            nextCellIndex = neighbours[0];
        }
        else
        {
            float walkLeft = -1;
            float walkForwardLeft = -1;
            float walkRight = -1;
            float walkDownRight = -1;
            if (neighbours[2] != -1)
            {
                walkLeft = Random.Range(0.0f, 100.0f) * walkLeftWeight;
            }
            if (neighbours[1] != -1)
            {
                walkForwardLeft = Random.Range(0.0f, 100.0f) * walkForwardLeftWeight;
            }
            if (neighbours[6] != -1)
            {
                walkRight = Random.Range(0.0f, 100.0f) * walkRightWeight;
            }
            if (neighbours[7] != -1)
            {
                walkDownRight = Random.Range(0.0f, 100.0f) * walkForwardRightWeight;
            }

            float highestRoll = Mathf.Max(Mathf.Max(walkLeft, walkForwardLeft)
                , Mathf.Max(walkRight, walkDownRight));

            if (highestRoll == walkLeft)
            {
                nextCellIndex = neighbours[2];
            }
            else if (highestRoll == walkForwardLeft)
            {
                nextCellIndex = neighbours[1];
            }
            else if (highestRoll == walkRight)
            {
                nextCellIndex = neighbours[6];
            }
            else if (highestRoll == walkDownRight)
            {
                nextCellIndex = neighbours[7];
            }
        }
        return nextCellIndex;
    }
    public void AlignToPlayerDirection(int[] neighbours)
    {
        int index = GetPlayerDirectionAsIndex();

        AssignDirection(neighbours, index);
    }
    int GetPlayerDirectionAsIndex()
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 toPlayerDir = (playerPos - currentCellPos).normalized;
        int index = 0;
        float range = 0.4f;
        bool xRight = toPlayerDir.x > range;
        bool xLeft = toPlayerDir.x < -range;
        bool zDown = toPlayerDir.z < -range;
        bool zUp = toPlayerDir.z > range;

        if (xRight)
        {
            if (zDown)
            {
                index = 7;
            }
            else if (zUp)
            {
                index = 5;
            }
            else
            {
                index = 6;
            }
        }
        else if (xLeft)
        {
            if (zDown)
            {
                index = 1;
            }
            else if (zUp)
            {
                index = 3;
            }
            else
            {
                index = 2;
            }
        }
        else
        {
            if (zDown)
            {
                index = 0;
            }
            else if (zUp)
            {
                index = 4;
            }
        }


        return index;
    }
    void AssignDirection(int[] neighbours, int startIndex)
    {
        int backLeft, back, backRight, right, forwardRight, forward, forwardLeft, left;
        int index = startIndex;
        forward = neighbours[index];
        index++;
        if (index >= 8)
        {
            index = 0;
        }
        forwardLeft = neighbours[index];
        index++;
        if (index >= 8)
        {
            index = 0;
        }
        left = neighbours[index];
        index++;
        if (index >= 8)
        {
            index = 0;
        }
        backLeft = neighbours[index];
        index++;
        if (index >= 8)
        {
            index = 0;
        }
        back = neighbours[index];
        index++;
        if (index >= 8)
        {
            index = 0;
        }
        backRight = neighbours[index];
        index++;
        if (index >= 8)
        {
            index = 0;
        }
        right = neighbours[index];
        index++;
        if (index >= 8)
        {
            index = 0;
        }
        forwardRight = neighbours[index];

        neighbours[0] = forward;
        neighbours[1] = forwardLeft;
        neighbours[2] = left;
        neighbours[3] = backLeft;
        neighbours[4] = back;
        neighbours[5] = backRight;
        neighbours[6] = right;
        neighbours[7] = forwardRight;
    }
    public void RemoveEnemyFromGroup(Enemy enemy)
    {
        enemy.transform.SetParent(null);
        enemies.Remove(enemy);
        if (enemies.Count <= 0)
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
