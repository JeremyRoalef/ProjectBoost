using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float fltPosOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Vector3 spawnPos = transform.position + (transform.forward * fltPosOffset);
        GameObject projectileObj = Instantiate(projectile, spawnPos, Quaternion.identity);
        projectileObj.GetComponent<ProjectileMovement>().SetProjectileVelocity(transform.up);
        projectileObj.GetComponent<ProjectileMovement>().SetProjectileDamage(GetComponent<HealthSystem>().DealDamage());
        projectileObj.GetComponent<ProjectileMovement>().SetProjectileTag("Player");
    }
}
