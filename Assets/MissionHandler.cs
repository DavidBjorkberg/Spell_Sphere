using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionHandler : MonoBehaviour
{
    internal Mission currentMission;
    public Text progressText;
    int nrOfEnemies;
    public void UpdateText()
    {
        nrOfEnemies--;
        progressText.text = nrOfEnemies + "/" + currentMission.nrOfEnemiesToKill + "Enemies left";
    }
    public void InitText()
    {
        nrOfEnemies = currentMission.nrOfEnemiesToKill;
        progressText.gameObject.SetActive(true);
        progressText.text = nrOfEnemies + "/" + currentMission.nrOfEnemiesToKill + "Enemies left";
    }
}

