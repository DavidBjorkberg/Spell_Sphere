using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct : MonoBehaviour
{
    private int totalNrOfParts;
    private int curNrOfParts = 0;
    internal bool constructionComplete;

    private void Awake()
    {
        totalNrOfParts = transform.GetChild(0).childCount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root != transform && other.TryGetComponent(out Robot robot))
        {
            if (robot.threat.gameObject == gameObject)
            {
                other.gameObject.SetActive(false);
                ActivatePart();
            }
        }
    }
    void ActivatePart()
    {
        transform.GetChild(0).GetChild(curNrOfParts).gameObject.SetActive(true);
        curNrOfParts++;
        if (curNrOfParts >= totalNrOfParts)
        {
            FinishedConstruction();
        }
    }
    void FinishedConstruction()
    {
        int nrOfAliveParts = 0;
        for (int i = 0; i < transform.GetChild(0).transform.childCount; i++)
        {
            if (transform.GetChild(0).GetChild(i).gameObject.activeSelf)
            {
                nrOfAliveParts++;
            }
        }
        GetComponent<ThreatHealth>().FinishedConstruction(nrOfAliveParts);
        constructionComplete = true;
    }
}
