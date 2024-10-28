using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    //get object's damage from projectile movement script
    int intDamage;

    private void Start()
    {
        intDamage = GetComponent<ProjectileMovement>().GetProjectileDamage();
        //Debug.Log("Damage: " + intDamage);
    }
    void OnTriggerEnter(Collider other)
    {
        //switch logic based on if the game object's tag is player or dangerous
        switch (gameObject.tag)
        {
            case "Player":
                UsePlayerCollision(other);
                break;
            case "Dangerous":
                UseEnemyCollision(other);
                break;
        }
    }

    void UsePlayerCollision(Collider other)
    {
        if (other.gameObject.tag == "Dangerous")
        {
            Debug.Log("Enemy HP: " + other.gameObject.GetComponent<HealthSystem>().getHp());
            //get the object's health system & deal damage to it
            if (other.gameObject.GetComponent<HealthSystem>().TakeDamage(intDamage))
            {
                Debug.Log("Enemy Dead: " + other.gameObject.GetComponent<HealthSystem>().getHp());
                Destroy(other.gameObject);
            }
        }
        Destroy(gameObject);
    }
    
    void UseEnemyCollision(Collider other)
    {

    }
}
