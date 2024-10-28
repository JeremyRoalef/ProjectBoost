using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] float fltMoveSpeed = 10f;
    
    Rigidbody projectileRb;

    int intProjectileDamage = 0;


    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //SetProjectileVelocity(Vector3.up);
    }

    //Method to set the projectile velocity to go in a certain direction
    public void SetProjectileVelocity(Vector3 direction)
    {
        projectileRb.velocity = direction * fltMoveSpeed;
    }

    //Method to set the projectile damage based on the object's damage value
    public void SetProjectileDamage(int projectileDamage)
    {
        intProjectileDamage = projectileDamage;
    }

    //Method to set the tag of the object to determine if it should harm the player or the enemies
    public void SetProjectileTag(string tagName)
    {
        gameObject.tag = tagName;
    }

    public int GetProjectileDamage() { return intProjectileDamage; }
}
