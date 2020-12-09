using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpawnRobots : MonoBehaviour
{
    public float minSpawnDistance;
    public float maxSpawnDistance;
    public Robot robotPrefab;
    public float spawnTime;
    private List<Robot> robots = new List<Robot>();
    private float spawnTimer;
    private Transform myTransform;
    private int nrOfSpawnedRobots;
    private int nrOfRobotsToSpawn;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        nrOfRobotsToSpawn = transform.GetChild(0).childCount;
        for (int i = 0; i < nrOfRobotsToSpawn; i++)
        {
            robots.Add(SpawnRobot());
        }
    }
    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0 && nrOfSpawnedRobots < nrOfRobotsToSpawn)
        {
            robots[nrOfSpawnedRobots].gameObject.SetActive(true);
            robots[nrOfSpawnedRobots].Initialize(myTransform.position);
            nrOfSpawnedRobots++;
            spawnTimer = spawnTime;
        }

    }
    public void RobotDied()
    {
        nrOfRobotsToSpawn++;
        robots.Add(SpawnRobot());
    }
    Robot SpawnRobot()
    {

        Vector2 randomDir;
        float randomDistance;
        Vector2 randomPoint;
        Vector3 randomPointAroundConstruction;
        do
        {
            randomDir = Random.insideUnitCircle.normalized;
            randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            randomPoint = randomDir * randomDistance;

            randomPointAroundConstruction = new Vector3(transform.position.x + randomPoint.x, transform.position.y, transform.position.z + randomPoint.y);

        } while (!NavMesh.SamplePosition(randomPointAroundConstruction, out NavMeshHit hit, 1, NavMesh.AllAreas));

        Robot robotGO = Instantiate(robotPrefab, randomPointAroundConstruction, Quaternion.identity);
        robotGO.spawner = this;
        robotGO.threat = GetComponent<ThreatHealth>();
        robotGO.gameObject.SetActive(false);

        return robotGO;

    }
}