using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;
    public Camera playerCamera;
    public Text scoreText;
    public Text explodeText;
    public Text rotateText;
    public Text doubleText;
    public PlayerCombat tearCombat;
    public MissionHandler missionHandler;
    internal int score;
    bool hasExplode;
    bool hasRotate;
    bool hasDouble;
    private void Awake()
    {
        Instance = this;
        //scoreText.text = "Score: 0";
        //if (Instance == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (hasExplode)
            {

                hasExplode = false;
            }
            else
            {
                hasExplode = true;
            }
        }

    }
    void RemoveEffect(string effectName)
    {
        for (int i = 0; i < player.playerCombat.tearEffects.Count; i++)
        {
            if (player.playerCombat.tearEffects[i].ToString() == effectName)
            {
                player.playerCombat.tearEffects.RemoveAt(i);
                break;
            }
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        //scoreText.text = "Score: " + score.ToString();
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
