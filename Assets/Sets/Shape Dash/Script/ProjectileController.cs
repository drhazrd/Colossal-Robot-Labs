using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 20f;
    private ShapeDashColor projectileColor;

    private Rigidbody2D rb;
    public AudioClip sfxClip;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = transform.up * speed;
    }


    public void SetColor(ShapeDashColor color)
    {
        projectileColor = color;
        ColorSetup(projectileColor);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.transform.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage, projectileColor);
            if(sfxClip != null) AudioManager.instance.PlaySFXClip(sfxClip);

            Destroy(gameObject);
        }
        if(collision.tag == "Scenery"){
            Destroy(gameObject);
        }

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
