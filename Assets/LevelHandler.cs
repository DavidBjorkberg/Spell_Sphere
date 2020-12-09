using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public GameObject constructPrefab;
    public List<Transform> constructSpawnPoints;
    public Transform playerLevelSpawnPos;
    public Transform playerTownSpawnPos;
    private MissionHandler missionHandler;
    private int nrOfAliveEnemies;
    private void Awake()
    {
        missionHandler = GetComponent<MissionHandler>();
    }
    public void StartLevel()
    {
        GameManager.Instance.player.transform.position = playerLevelSpawnPos.position;
        for (int i = 0; i < missionHandler.currentMission.nrOfEnemiesToKill; i++)
        {
            Instantiate(constructPrefab, constructSpawnPoints[i].position, Quaternion.identity);
            nrOfAliveEnemies++;
        }
        GameManager.Instance.missionHandler.InitText();
    }
    public void EndLevel()
    {
        string rewardName = missionHandler.currentMission.reward.GetName();
        GameManager.Instance.player.playerCombat.tearEffects.Add(ScriptableObject.CreateInstance(rewardName) as TearEffect);

        missionHandler.progressText.gameObject.SetActive(false);
        GameManager.Instance.player.transform.position = playerTownSpawnPos.position;
    }
    public void ConstructDied()
    {
        missionHandler.UpdateText();
        nrOfAliveEnemies--;
        if (nrOfAliveEnemies <= 0)
        {
            Invoke("EndLevel", 2);
        }
    }
}
