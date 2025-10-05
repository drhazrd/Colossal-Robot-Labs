using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wController;
    public GameObject hitParticle;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && wController.isAttacking == true)
        {
            Debug.Log(other.name);

            other.GetComponent<EnemyHealth>().TakeDamage();

            //Instantiate(hitParticle, other.transform);

            GetComponent<Collider>().enabled = false;
        }
    }
}
