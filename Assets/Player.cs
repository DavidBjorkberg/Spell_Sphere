using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    internal PlayerMovement playerMovement;
    internal PlayerCombat playerCombat;
    internal AttackSpots attackSpots;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        attackSpots = GetComponent<AttackSpots>();
    }
}
