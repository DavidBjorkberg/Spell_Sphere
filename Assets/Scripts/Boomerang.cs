using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TearEffect/Boomerang")]

public class Boomerang : TearEffect
{
    float turnSpeed = 0.7f;
    float turnRange = 8;
    Vector3 initialDir;
    Vector3 initialPos;
    float lerpValue = 0;
    public override void OnInitialize(Tear tear)
    {
        initialPos = tear.transform.position;
        initialDir = tear.direction;
    }
    public override void OnMove(Tear tear)
    {
        float x1 = initialPos.x;
        float x2 = tear.transform.position.x;
        float y1 = initialPos.y;
        float y2 = tear.transform.position.y;
        float z1 = initialPos.z;
        float z2 = tear.transform.position.z;
        if (GameManager.Instance.GetSquaredDistanceBetweenTwoPointsXYZ(x1, x2, y1, y2, z1, z2) >= turnRange * turnRange)
        {
            if (lerpValue < 1)
            {
                lerpValue += Time.deltaTime * turnSpeed;
                tear.direction = Vector3.Lerp(initialDir, -initialDir, lerpValue);
            }
        }
    }
    public override string GetName()
    {
        return "Boomerang";
    }
}
