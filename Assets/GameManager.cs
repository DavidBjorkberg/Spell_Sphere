using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;
    public Camera playerCamera;
    internal GridHandler gridHandler;
    private void Awake()
    {
        if(Instance == null)
        {
            gridHandler = GetComponent<GridHandler>();
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public float GetCheapDistanceBetweenTwoPoints(float x1,float x2,float z1,float z2)
    {
        return (x1 - x2) + (z1 - z2);
    }
}
