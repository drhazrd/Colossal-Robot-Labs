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
    public AudioClip fireSFX;


    [Header("Teleportation")]
    public TeleportManager teleportManager;
    public float teleportCooldown = 1f;
    private float nextTeleportTime;
    public float teleportCost = 10f;
    public AudioClip teleportSFX;


    [Header("Player State")]
    public ShapeDashColor playerColor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ColorSetup(playerColor);
        teleportManager = GameManager.Instance.teleportManager;
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleTeleport();
    }

 
    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed;
        rb.velocity = movement;
        //graphics to face the movement direction
        if (movement.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            if (graphicsTransform != null)
            {
                // use Eulerangles to set the rotation directly
                graphicsTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            }
        }
    }

    private void HandleShooting()
    {
        // mouse
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


    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;
            ProjectileController projectileScript = projectile.GetComponent<ProjectileController>();
            if (projectileScript != null)
            {
                AudioManager.instance.PlaySFXClip(fireSFX);
                projectileScript.SetColor(playerColor);
            }
        }
    }

    private void HandleTeleport()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextTeleportTime && GameManager.Instance.karmaMeter >= teleportCost)
        {
            Teleport();
            nextTeleportTime = Time.time + teleportCooldown;
        }

        if (GameManager.Instance.karmaMeter < 0 && Random.Range(0f, 100f) < Mathf.Abs(GameManager.Instance.karmaMeter) * 0.1f)
        {
            Teleport();
            GameManager.Instance.ChangeKarma(-10f); 
        }
    }


    private void Teleport()
    {
        Vector2 newPosition = teleportManager.GetSafeTeleportPosition(transform.position);
        transform.position = newPosition;
        AudioManager.instance.PlaySFXClip(teleportSFX);
        GameManager.Instance.ChangeKarma(-teleportCost); // karma cost

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
    void OnDestroy(){
        if(GameManager.Instance != null) GameManager.Instance.ResetGameScreen();
    }
}

public enum ShapeDashColor
{
    Red,
    Green,
    Blue,
    Yellow
}
