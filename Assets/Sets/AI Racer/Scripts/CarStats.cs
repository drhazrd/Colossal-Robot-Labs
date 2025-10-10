using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStats : MonoBehaviour
{
    public float speedMax = 70f;
    public float speedMin = -50f;
    public float acceleration = 30f;
    public float brakeSpeed = 100f;
    public float reverseSpeed = 30f;
    public float idleSlowdown = 10f;

    public float turnSpeed;
    public float turnSpeedMax = 300f;
    public float turnSpeedAcceleration = 300f;
    public float turnIdleSlowdown = 500f;

}
