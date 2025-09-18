using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShapeMenu : MonoBehaviour
{
    public GameObject startMenu, creditsMenu, loseMenu, hudObject;
    public TextMeshProUGUI highscore, enemyCount;
    void Start()
    {
        SetStart();
    }
    public void SetStart(){
        startMenu.SetActive(true);
        loseMenu.SetActive(false);
        creditsMenu.SetActive(false);
        hudObject.SetActive(false);
        if(highscore != null) highscore.text = $"{PlayerPrefs.GetInt("playerScore", 0)}";
        if(enemyCount != null) enemyCount.text = $"{PlayerPrefs.GetInt("killCount", 0)}";
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
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif    
    }
}
