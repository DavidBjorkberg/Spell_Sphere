using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject player;
    public Camera playerCamera;
    public GridHandler gridHandler;
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
}
