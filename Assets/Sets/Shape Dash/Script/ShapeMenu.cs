using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShapeMenu : MonoBehaviour
{
    public GameObject startMenu, creditsMenu, loseMenu, hudObject;
    void Start()
    {
        SetStart();
    }
    public void SetStart(){
        startMenu.SetActive(true);
        loseMenu.SetActive(false);
        creditsMenu.SetActive(false);
        hudObject.SetActive(false);
    }
    public void StartGame(){
        Clear();
        hudObject.SetActive(true);
        GameManager.Instance.GameStart();
    }
    public void OpenCredits(){
        startMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    public void Clear(){
        startMenu.SetActive(false);
        creditsMenu.SetActive(false);
        loseMenu.SetActive(false);
    }
    public void Lose(){
        loseMenu.SetActive(true);
        hudObject.SetActive(false);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
