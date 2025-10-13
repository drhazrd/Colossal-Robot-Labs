using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriverAI : MonoBehaviour {

    [SerializeField] private Transform targetPositionTranform;
    List<Transform> path = new List<Transform>();
    public float reachedTargetDistance = 5f;
    public List<Transform> targetTransforms = new List<Transform>();

    private CarDriver carDriver;
    private Vector3 targetPosition;
    private int currentLoops;
    [SerializeField] private int maxLoops = 3;
    private int currentTargetIndex = 0;

    void OnEnable()
    {
        carDriver = GetComponent<CarDriver>();
        RaceManager.manager.RegisterRacer(this);
    }

    private void Update() {
        if (targetTransforms.Count > 0)
        {
            FollowTargets(targetTransforms);
        }else if(targetPositionTranform) SetTargetPosition(targetPositionTranform.position);

        if (currentLoops >= maxLoops)
        {
            carDriver.SetInputs(0, 0);
            return;
        }

        float forwardAmount = 0f;
        float turnAmount = 0f;

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget > reachedTargetDistance) {
            // Still too far, keep going
            Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dirToMovePosition);

            if (dot > 0) {
                // Target in front
                forwardAmount = 1f;

                float stoppingDistance = 30f;
                float stoppingSpeed = 40f;
                if (distanceToTarget < stoppingDistance && carDriver.GetSpeed() > stoppingSpeed) {
                    // Within stopping distance and moving forward too fast
                    forwardAmount = -1f;
                }
            } else {
                // Target behind
                float reverseDistance = 25f;
                if (distanceToTarget > reverseDistance) {
                    // Too far to reverse
                    forwardAmount = 1f;
                } else {
                    forwardAmount = -1f;
                }
            }

            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

            if (angleToDir > 0) {
                turnAmount = 1f;
            } else {
                turnAmount = -1f;
            }
        } else {
            // Reached target
            if (carDriver.GetSpeed() > 15f) {
                forwardAmount = -1f;
            } else {
                forwardAmount = 0f;
            }
            turnAmount = 0f;
        }

        carDriver.SetInputs(forwardAmount, turnAmount);
    }

    private void FollowTargets(List<Transform> targets)
    {
        if (targets.Count == 0) return;

        targetPosition = targets[currentTargetIndex].position;

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget < reachedTargetDistance)
        {
            currentTargetIndex++;
            if (currentTargetIndex >= targets.Count)
            {
                currentTargetIndex = 0; // Loop back to start
                currentLoops++;
            }
        }
    }
    public void SetTargetPosition(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        foreach (Transform t in targetTransforms) {
            Gizmos.DrawSphere(t.position, 1f);
        }
    }
    void OnDestroy(){
        RaceManager.manager.UnRegisterRacer(this);
    }
}
