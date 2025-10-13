using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceManager : MonoBehaviour
{
    public static RaceManager manager;
    public List<CarDriverAI> racers = new List<CarDriverAI>();
    public List<RaceCheckpoint> checkpoints = new List<RaceCheckpoint>();

    public TextMeshProUGUI displayText;

    public void RegisterRacer()
    {

    }
    public void NewTurn(CarDriverAI raceLeader)
    {
        //Update racer list to textmesh protext for all of the racers and their places relative to the leader.
        StartTurn();

    }

    public void StartTurn()
    {
        //Start the turn by pausing the game
        //let players roll dice then heads to ProcessTurn()
    }
    public void ProcessTurn()
    {
        //allow racer ai actions and contuines to the EndTurn()
    }
    public void EndTurn()
    {  
        //Un-Pauses game
    }

    void Start()
    {
        if (RaceManager.manager == null)
        {
            manager = this;
        }
        else Destroy(this);
    }


}
