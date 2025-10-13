using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCheckpoint : MonoBehaviour
{
    public Action checkpointActivated;
    public Material idleMaterial;
    public Material activeMaterial;
    Renderer mat;

    public bool pointActive { get; private set; }

    void Start()
    {
        mat = GetComponent<Renderer>();
        mat.material = idleMaterial;
    }

    IEnumerator UpdateStatus(CarDriverAI leader)
    {
        RaceManager.manager.NewTurn(leader);
        yield return new WaitForSeconds(.5f);
        mat.material = idleMaterial;

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<CarDriverAI>(out CarDriverAI driver))
        {
            mat.material = activeMaterial;
            StartCoroutine(UpdateStatus(driver));
        }
    }
}
