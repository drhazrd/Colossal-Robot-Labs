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
        mat = GetComponentInChildren<Renderer>();
        mat.material = idleMaterial;
        ActiveStatus(true);
    }

    IEnumerator UpdateStatus(CarDriverAI leader)
    {
        RaceManager.manager.NewTurn(leader);
        yield return new WaitForSeconds(.5f);
        mat.material = idleMaterial;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<CarDriverAI>(out CarDriverAI driver) && pointActive)
        {        
            ActiveStatus(false);
            mat.material = activeMaterial;
            StartCoroutine(UpdateStatus(driver));
        }
    }
    public void ActiveStatus(bool status){
        pointActive = status;
    }
}
