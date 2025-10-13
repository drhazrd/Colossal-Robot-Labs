using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceManager : MonoBehaviour
{
    public static RaceManager manager;
    public List<CarDriverAI> racers = new List<CarDriverAI>();
    CarDriverAI firstPlace;
    public List<RaceCheckpoint> checkpoints = new List<RaceCheckpoint>();
    public RaceState state;
    public TextMeshProUGUI displayText;
    float turnTimer;

    public void RegisterRacer(CarDriverAI racer)
    {
        racers.Add(racer);
    }
    public void UnRegisterRacer(CarDriverAI racer)
    {
        racers.Remove(racer);
    }
    public void NewTurn(CarDriverAI raceLeader)
    {
        //Update racer list to textmesh protext for all of the racers and their places relative to the leader.
        firstPlace = raceLeader;
        StartTurn();

    }

    public void StartTurn()
    {
        //Start the turn by pausing the game
        //let players roll dice then heads to 
        // ProcessTurn()
        SetNewState(RaceState.Paused);
        StartCoroutine(ProcessTurn());
    }
    IEnumerator ProcessTurn()
    {
        //allow racer ai actions and contuines to the 
        // EndTurn()
        float f = .05f;
        turnTimer = f;
        yield return new WaitForSeconds(f);
        EndTurn();
    }
    public void EndTurn()
    {  
        //Un-Pauses game
        turnTimer = 0;
        SetNewState(RaceState.Racing);
    }

    public void Roll(){
        int n = UnityEngine.Random.Range(1,20);
        Debug.Log($"Rolled {n}");
    }

    void Awake()
    {
        if (RaceManager.manager == null)
        {
            manager = this;
        }
        else Destroy(this);
        checkpoints.AddRange(this.GetComponentsInChildren<RaceCheckpoint>());
    }
    void Update(){
        if(turnTimer > 0){
            turnTimer -= Time.deltaTime;
        }else if(turnTimer<=0){
            turnTimer = 0;
        }
        if (displayText != null) DisplayText();
    }
    void DisplayText(){
        displayText.text = $"{firstPlace} is in First Place \n Turn TImer: {turnTimer} \n State: {state}";
    }
    void SetNewState(RaceState newState){
        state = newState;
        switch (state)
        {
            case RaceState.Racing:
            Time.timeScale = 1f;
            break;
            
            case RaceState.Paused:
            Time.timeScale = .01f;
            break;
        }
    }
}
public enum RaceState
{
    Racing,
    Paused
}