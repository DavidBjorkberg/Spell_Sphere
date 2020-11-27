﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpots : MonoBehaviour
{
    public Vector3[] spots;
    public float spotOffset;
    internal bool[] spotTaken = new bool[8];
    /// <summary>
    /// Returns Vector3.zero if no spots are available.
    /// </summary>
    public int GetAndClaimClosestSpot(Vector3 pos)
    {
        float xDelta;
        float zDelta;
        float distance;
        int closestIndex = -1;
        float closestDistance = -1;
        for (int i = 0; i < spots.Length; i++)
        {
            if (spotTaken[i])
                continue;

            xDelta = spots[i].x - pos.x;
            zDelta = spots[i].z - pos.z;
            distance = xDelta + zDelta;
            if (distance < closestDistance || closestDistance == -1)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }
        if (closestIndex != -1)
        {
            spotTaken[closestIndex] = true;
        }
        return closestIndex;
    }
    public Vector3 GetSpotPos(int index)
    {
        return spots[index];
    }
    public void UnclaimSpot(int index)
    {
        spotTaken[index] = false;
    }
    public int GetNrOfFreeSpots()
    {
        int nrOfFreeSpots = 0;
        for (int i = 0; i < spotTaken.Length; i++)
        {
            if(!spotTaken[i])
            {
                nrOfFreeSpots++;
            }
        }
        return nrOfFreeSpots;
    }
    private void OnDrawGizmos()
    {//TODO: Turn off collisions for enemy when moving to spot.
        for (int i = 0; i < 8; i++)
        {
            Gizmos.DrawWireSphere(transform.position + (spots[i] * spotOffset), 0.3f);
        }
    }

}
