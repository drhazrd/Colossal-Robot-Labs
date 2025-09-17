using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float speed = 1.25f;
    public float health = 100f;
    public int scoreValue = 10;
    public float karmaChangeOnDeath = 5f;
    public ShapeDashColor enemyColor;

    private Transform playerTransform;

    private void Start()
    {
        // Find the player object
        playerTransform = GameManager.Instance.player.transform;
        ColorSetup(enemyColor);
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Move towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Applies damage to the enemy.
    /// </summary>
    public void TakeDamage(float damage, ShapeDashColor projectileColor)
    {
        // Check for color match
        if (projectileColor == enemyColor)
        {
            damage *= 2; // Double damage on color match
            // Add a positive karma effect for good color matching
            GameManager.Instance.ChangeKarma(2f);
        }

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles enemy death and updates the game state.
    /// </summary>
    private void Die()
    {
        GameManager.Instance.AddScore(scoreValue);
        GameManager.Instance.EnemyDied();
        GameManager.Instance.ChangeKarma(karmaChangeOnDeath);

        // Here you would add an explosion effect or sound
        Destroy(gameObject);
    }
    public void ColorSetup(ShapeDashColor type){
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        
        switch (type)
        {
            case ShapeDashColor.Red:
                render.color = Color.red;
            break;
            case ShapeDashColor.Blue:
                render.color = Color.blue;
            break;
            case ShapeDashColor.Green:
                render.color = Color.green;
            break;
            case ShapeDashColor.Yellow:
                render.color = Color.yellow;
            break;

            default:
                render.color = Color.gray;
            break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthController enemy = collision.transform.GetComponent<HealthController>();
        int damage = 1;
        if (enemy != null)
        {
            enemy.TakeDamage();
        }
    }
}
