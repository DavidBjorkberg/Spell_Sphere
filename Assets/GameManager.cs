using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;
    public Camera playerCamera;
    internal GridHandler gridHandler;
    public Text scoreText;
    internal int score;
    private void Awake()
    {
        Instance = this;
        gridHandler = GetComponent<GridHandler>();
        scoreText.text = "Score: 0";
        //if (Instance == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
    }
    public float GetCheapDistanceBetweenTwoPointsXZ(float x1, float x2, float z1, float z2)
    {
        return (x1 - x2) + (z1 - z2);
    }
    public float GetSquaredDistanceBetweenTwoPointsXZ(float x1, float x2, float z1, float z2)
    {
        float deltaX = x1 - x2;
        float deltaZ = z1 - z2;

        return (deltaX * deltaX) + (deltaZ * deltaZ);
    }
    public float GetSquaredDistanceBetweenTwoPointsXYZ(float x1, float x2, float y1, float y2, float z1, float z2)
    {
        float deltaX = x1 - x2;
        float deltaY = y1 - y2;
        float deltaZ = z1 - z2;

        return (deltaX * deltaX) + (deltaY * deltaY) + (deltaZ * deltaZ);
    }
}
