using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxHeart = 3;
    int currentHeart;
    
    public void TakeDamage()
    {
        if(currentHeart > maxHeart){
            currentHeart = 0;
            Die();
        } else
        {
            currentHeart--;
        }
    }

    void Die()
    {
        Destroy(gameObject, .1f);
    }
    void Start(){
        currentHeart = maxHeart;
    }
}
