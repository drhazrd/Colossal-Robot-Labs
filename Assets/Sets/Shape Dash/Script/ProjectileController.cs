using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 20f;
    private ShapeDashColor projectileColor;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Apply forward velocity based on the rotation of the fire point
        rb.velocity = transform.up * speed;
    }

    /// <summary>
    /// Sets the projectile's color.
    /// </summary>
    public void SetColor(ShapeDashColor color)
    {
        projectileColor = color;
        ColorSetup(projectileColor);
        // You would change the SpriteRenderer color here
        // GetComponent<SpriteRenderer>().color = GetColorFromEnum(color);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.transform.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage, projectileColor);
            Destroy(gameObject);
        }else Destroy(gameObject);


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
}
