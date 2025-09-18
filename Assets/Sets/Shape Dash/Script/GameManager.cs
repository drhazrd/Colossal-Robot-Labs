using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TeleportManager teleportManager{ get; private set; }

    public int score = 0;
    public float karmaMeter = 0;
    public int enemyCount = 0;
    private const float karmaResetRate = 2.5f; 

    [Header("Game Objects")]
    public GameObject player;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI karmaText;

    public ShapeMenu ui;
    public float spawnDelay = 2f;
    bool gameActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        teleportManager = GetComponent<TeleportManager>();
    }

    public void GameStart()
    {
        ui = GetComponent<ShapeMenu>();
        UpdateScoreText();
        UpdateEnemyCountText();
        UpdateKarmaText();
        NewPlayer();
        AudioManager.instance.PlayBGM();

        gameActive = true;
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        karmaMeter = Mathf.Lerp(karmaMeter, 0, Time.deltaTime * karmaResetRate);
        UpdateKarmaText();
    }


    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void EnemyDied()
    {
        enemyCount++;
        UpdateEnemyCountText();
    }


    public void ChangeKarma(float amount)
    {
        karmaMeter += amount;
        karmaMeter = Mathf.Clamp(karmaMeter, -100, 100);
    }

    private IEnumerator SpawnEnemies()
    {
        while (gameActive)
        {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
                UpdateEnemyCountText();
                spawnDelay += 0.25f;
            yield return new WaitForSeconds(spawnDelay); 
        }
    }

    public void ResetGameScreen(){
        ui.Lose();
        PlayerPrefs.SetInt("playerScore", score);
        PlayerPrefs.SetInt("killCount", enemyCount);
        gameActive = false;
        // Find and destroy all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        AudioManager.instance.StopBGM();
    }
    public void ResetGame()
    {
        score = 0;
        karmaMeter = 0;
        enemyCount = 0;
        // Reposition player
        NewPlayer();
        // Update UI
        UpdateScoreText();
        UpdateEnemyCountText();
        UpdateKarmaText();
        transform.GetComponent<ShapeMenu>().Clear();
        AudioManager.instance.PlayBGM();

        gameActive = true;
        StartCoroutine(SpawnEnemies());
    }
    public void LeaveGame()
    {
        score = 0;
        karmaMeter = 0;
        enemyCount = 0;
        // Reposition player
        transform.GetComponent<ShapeMenu>().SetStart();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    public void NewPlayer(){
        player = Instantiate(playerPrefab, transform.position, transform.rotation) as GameObject;
        player.transform.position = Vector3.zero;
    }

    private void UpdateEnemyCountText()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemies: " + enemyCount;
        }
    }

    private void UpdateKarmaText()
    {
        if (karmaText != null)
        {
            karmaText.text = "Karma: " + karmaMeter.ToString("F0");
        }
    }
}
