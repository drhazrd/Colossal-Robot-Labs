using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public int damage;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage()
    {
        health -= damage;

        Debug.Log(health);
    }
}
