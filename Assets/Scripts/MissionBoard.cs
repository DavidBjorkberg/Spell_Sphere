using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionBoard : Interactable
{
    public List<Mission> missions = new List<Mission>();
    public Button leftButton;
    public Button rightButton;
    public FPSCamera playerCam;
    public GameObject missionSelect;
    public MissionHandler missionHandler;
    public LevelHandler levelHandler;
    private Mission mission1;
    private Mission mission2;
    private void Start()
    {
        RandomizeMissions();
    }
    public override void Interact()
    {
        playerCam.interacting = true;
        missionSelect.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    void RandomizeMissions()
    {
        int randomMission = Random.Range(0, missions.Count);
        leftButton.transform.GetChild(0).GetComponent<Text>().text = "Enemies to kill: " + missions[randomMission].nrOfEnemiesToKill;
        leftButton.transform.GetChild(1).GetComponent<Text>().text = "Reward: " + missions[randomMission].reward.GetName();
        mission1 = missions[randomMission];

        randomMission = Random.Range(0, missions.Count);
        rightButton.transform.GetChild(0).GetComponent<Text>().text = "Enemies to kill: " + missions[randomMission].nrOfEnemiesToKill;
        rightButton.transform.GetChild(1).GetComponent<Text>().text = "Reward: " + missions[randomMission].reward.GetName();
        mission2 = missions[randomMission];
    }
    public void ChooseLeftMission()
    {
        missionHandler.currentMission = mission1;
        levelHandler.StartLevel();
        playerCam.interacting = false;
        missionSelect.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ChooseRightMission()
    {
        missionHandler.currentMission = mission2;
        levelHandler.StartLevel();
        playerCam.interacting = false;
        missionSelect.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
