using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatHealth : MonoBehaviour
{
    public float deathThresholdInPercent;
    private int curNrOfParts;
    private int deathThresholdInParts;
    private Construct construct;
    private void Awake()
    {
        construct = GetComponent<Construct>();
        curNrOfParts = transform.GetChild(0).childCount;
        deathThresholdInParts = Mathf.FloorToInt(curNrOfParts * deathThresholdInPercent);
    }
    public void PartDied()
    {
        if (construct.constructionComplete)
        {
            curNrOfParts--;
            if (curNrOfParts <= deathThresholdInParts)
            {
                GameManager.Instance.GetComponent<LevelHandler>().ConstructDied();
                gameObject.SetActive(false);
            }
        }
    }
    public void FinishedConstruction(int nrOfAliveParts)
    {
        curNrOfParts = nrOfAliveParts;
        deathThresholdInParts = Mathf.FloorToInt(curNrOfParts * deathThresholdInPercent);
    }
}
