using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject hitParticle;
    public GameObject camPivot;
    private CameraShake cameraShake;
    private WeaponController wController;

    void Start()
    {
        cameraShake = camPivot.GetComponent<CameraShake>();
        wController = transform.root.GetComponent<WeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && wController.IsAttacking == true)
        {
            Debug.Log(other.name);

            other.GetComponent<EnemyHealth>().TakeDamage();

            cameraShake.LightScreenShake();
            //Instantiate(hitParticle, other.transform);

            GetComponent<Collider>().enabled = false;
        }
    }
}
