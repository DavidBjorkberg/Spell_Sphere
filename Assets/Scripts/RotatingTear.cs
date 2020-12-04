using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTear : TearEffect
{
    Vector3 turnDirection;
    float turnSpeed = 6;

    public override void OnMove(Tear tear)
    {
        turnDirection += tear.transform.right * turnSpeed * Time.deltaTime;
        turnDirection.Normalize();
        tear.transform.position += turnDirection * tear.baseSpeed * Time.deltaTime;
        tear.transform.LookAt(tear.transform.position + turnDirection);
    }
    public override void OnInitialize(Tear tear)
    {
        turnDirection = tear.direction;
    }
}
