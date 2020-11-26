using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    public int levelHeight;
    public int levelWidth;
    internal int nrOfCellsInWidth;
    internal int nrOfCellsInHeight;
    Vector3 topLeftPos;
    private List<GridCell> grid = new List<GridCell>();
    float cellWidth = 5;
    float cellHeight = 3;
    private void Awake()
    {
        topLeftPos = Vector3.zero + new Vector3(-levelWidth / 2, 0, levelHeight / 2);
        for (int i = 0; i < levelHeight; i += (int)cellHeight)
        {
            for (int j = 0; j < levelWidth; j += (int)cellWidth)
            {
                grid.Add(ScriptableObject.CreateInstance("GridCell") as GridCell);
                Vector3 cellPos = topLeftPos + new Vector3(cellWidth / 2, 0, -cellHeight / 2) + new Vector3(j, 0, -i);
                grid[grid.Count - 1].Initialize(grid.Count - 1, cellPos);
            }
        }
        nrOfCellsInWidth = levelWidth / (int)cellWidth;
        nrOfCellsInHeight = levelHeight / (int)cellHeight;
    }
    public Vector3 GetCellPos(int index)
    {
        return grid[index].pos;
    }
    public int GetCellIndexByPos(Vector3 pos)
    {
        int index = 0;
        //Offset (0,0,0) to top left of level
        pos += new Vector3((float)levelWidth / 2, 0, -(float)levelHeight / 2);
        index += Mathf.FloorToInt(pos.x / cellWidth);
        index += Mathf.Abs((Mathf.FloorToInt(pos.z / cellHeight) + 1) * nrOfCellsInWidth);

        return index;
    }
    public GridCell GetCell(int index)
    {
        return grid[index];
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns>Returns the neighbours going clockwise, starting at bottom mid. 
    /// -1 if no neighbour exists at that position</returns>
    public int[] GetCellNeighbours(int index)
    {
        bool atRightEdge = (index + 1) % nrOfCellsInWidth == 0;
        bool atTopEdge = index - nrOfCellsInWidth < 0;
        bool atLeftEdge = index % nrOfCellsInWidth == 0;
        bool atBottomEdge = index + nrOfCellsInWidth > nrOfCellsInWidth * nrOfCellsInHeight;
        int[] neighbours = new int[8];

        for (int i = 0; i < 8; i++)
        {
            neighbours[i] = -1;
        }
        if (!atBottomEdge)
        {
            if (!atRightEdge && !grid[index + nrOfCellsInWidth + 1].isOccupied)
            {
                neighbours[7] = index + nrOfCellsInWidth + 1;
            }
            if (!grid[index + nrOfCellsInWidth].isOccupied)
            {
                neighbours[0] = index + nrOfCellsInWidth;
            }

            if (!atLeftEdge && !grid[index + nrOfCellsInWidth - 1].isOccupied)
            {
                neighbours[1] = index + nrOfCellsInWidth - 1;
            }
        }
        if (!atLeftEdge && !grid[index - 1].isOccupied)
        {
            neighbours[2] = index - 1;
        }
        if (!atTopEdge)
        {
            if (!atLeftEdge && !grid[index - nrOfCellsInWidth - 1].isOccupied)
            {
                neighbours[3] = index - nrOfCellsInWidth - 1;
            }
            if (!grid[index - nrOfCellsInWidth].isOccupied)
            {
                neighbours[4] = index - nrOfCellsInWidth;
            }

            if (!atRightEdge && !grid[index - nrOfCellsInWidth + 1].isOccupied)
            {
                neighbours[5] = index - nrOfCellsInWidth + 1;
            }
        }
        if (!atRightEdge && !grid[index + 1].isOccupied)
        {
            neighbours[6] = index + 1;
        }

        return neighbours;
    }

    private void OnDrawGizmos()
    {
        //Draw cell player is on
        //int index = GetCellIndexByPos(GameManager.Instance.player.transform.position);
        //Vector3 pos = GetCellPos(index);
        //Gizmos.DrawCube(pos, new Vector3(5, 1, 3));
    }
}
