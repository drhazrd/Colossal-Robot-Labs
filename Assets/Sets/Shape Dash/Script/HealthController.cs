using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxHeart = 3;
    int currentHeart;
    public GameObject deathVFX;
    public AudioClip deathSFX;

    
    public void TakeDamage()
    {
        if(currentHeart < maxHeart){
            currentHeart = 0;
            Die();
        } else
        {
            currentHeart--;
        }
    }

    void Die()
    {
        Debug.Log($"Death to {this.name}");
        if(deathVFX != null) Instantiate(deathVFX, transform.position, transform.rotation);
        if(deathSFX != null) AudioManager.instance.PlaySFXClip(deathSFX);

        Destroy(gameObject, .1f);
    }
    void Start(){
        currentHeart = maxHeart;
    }
}
