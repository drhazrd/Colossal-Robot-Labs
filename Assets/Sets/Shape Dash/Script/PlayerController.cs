using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform graphicsTransform;
    public Transform firePoint, firePivot;
    public float fireRate = 0.5f;
    private float nextFireTime;

    [Header("Teleportation")]
    public TeleportManager teleportManager;
    public float teleportCooldown = 1f;
    private float nextTeleportTime;
    public float teleportCost = 10f;

    [Header("Player State")]
    public ShapeDashColor playerColor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ColorSetup(playerColor);
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleTeleport();
    }

    /// <summary>
    /// Handles player movement using keyboard/controller input.
    /// </summary>
    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed;
        rb.velocity = movement;
        // Rotate the graphics to face the movement direction
        if (movement.magnitude > 0.1f) // Only rotate if there is significant movement
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            if (graphicsTransform != null)
            {
                // We use Euler angles to set the rotation directly
                graphicsTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            }
        }
    }

    /// <summary>
    /// Handles aiming and shooting projectiles.
    /// </summary>
    private void HandleShooting()
    {
        // Aiming with mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        firePivot.rotation = Quaternion.Euler(0, 0, angle);

        // Shooting logic
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    /// <summary>
    /// Instantiates a projectile and sets its color.
    /// </summary>
    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            ProjectileController projectileScript = projectile.GetComponent<ProjectileController>();
            if (projectileScript != null)
            {
                projectileScript.SetColor(playerColor);
            }
        }
    }

    /// <summary>
    /// Handles the teleport mechanic.
    /// </summary>
    private void HandleTeleport()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextTeleportTime && GameManager.Instance.karmaMeter >= teleportCost)
        {
            Teleport();
            nextTeleportTime = Time.time + teleportCooldown;
        }

        // Special teleportation for bad karma
        if (GameManager.Instance.karmaMeter < 0 && Random.Range(0f, 100f) < Mathf.Abs(GameManager.Instance.karmaMeter) * 0.1f)
        {
            Teleport();
            GameManager.Instance.ChangeKarma(-10f); // Add a small karma penalty for the random teleport
        }
    }

    /// <summary>
    /// Initiates a teleport to a new safe location.
    /// </summary>
    private void Teleport()
    {
        Vector2 newPosition = teleportManager.GetSafeTeleportPosition(transform.position);
        transform.position = newPosition;
        GameManager.Instance.ChangeKarma(-teleportCost); // Apply karma cost
        // Play a visual effect for teleport
        Debug.Log("Teleported to a new location!");
    }
    public void ColorSetup(ShapeDashColor type){
        SpriteRenderer render = firePoint.gameObject.GetComponent<SpriteRenderer>();
        
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
}

// Simple enum for managing colors
public enum ShapeDashColor
{
    Red,
    Green,
    Blue,
    Yellow
}
