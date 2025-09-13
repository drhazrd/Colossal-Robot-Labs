using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    int maxShots, currentShots;
    float timeBetweenShots, timeBetweenReloads;
    int damage;
    //ColorType currentColor;
    public GameObject bullet;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Fire(){
        if(bullet != null && firePoint != null){
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as GameObject;
        }
    }
}
