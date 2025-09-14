using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Singleton pattern for easy access from other scripts
    public static GameManager Instance { get; private set; }
    public TeleportManager teleportManager{ get; private set; }

    // Game state variables
    public int score = 0;
    public float karmaMeter = 0;
    public int enemyCount = 0;
    private const float karmaResetRate = 2.5f; // How quickly karma resets to 0

    [Header("Game Objects")]
    public GameObject player;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    [Header("UI References")]
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI enemyCountText;
    public TMPro.TextMeshProUGUI karmaText;

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
        // Initial setup for UI and spawning
        UpdateScoreText();
        UpdateEnemyCountText();
        UpdateKarmaText();
        NewPlayer();
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        // Slowly move karma back towards 0
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
        enemyCount--;
        UpdateEnemyCountText();
    }


    public void ChangeKarma(float amount)
    {
        karmaMeter += amount;
        karmaMeter = Mathf.Clamp(karmaMeter, -100, 100);
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (enemyCount < 10) // Example limit for enemy count
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
                enemyCount++;
                UpdateEnemyCountText();
            }
            yield return new WaitForSeconds(2f); // Wait 2 seconds before checking again
        }
    }

    public void ResetGame()
    {
        score = 0;
        karmaMeter = 0;
        enemyCount = 0;
        // Find and destroy all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        // Reposition player
        NewPlayer();
        // Update UI
        UpdateScoreText();
        UpdateEnemyCountText();
        UpdateKarmaText();
        transform.GetComponent<ShapeMenu>().Clear();
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
