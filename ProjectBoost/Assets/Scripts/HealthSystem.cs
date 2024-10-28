using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script will be applied to the player and enemies alike to give them health functionality.
 */


public class HealthSystem : MonoBehaviour
{
    //Serialized fields
    [SerializeField] ObjectScriptableObject objectSO; //Cashe reference to scriptable object to get health & damage values

    //Attributes
    int intMaxHp;
    int intHp;
    int intDamage;


    void Awake()
    {
        intMaxHp = objectSO.health;
        intHp = objectSO.health;
        intDamage = objectSO.damage;
    }

    //Method to reduce this object's hp. If the object's hp falls below 0, return true so the object can initiate death sequence
    public bool TakeDamage(int intDamageTaken)
    {
        intHp -= intDamageTaken;
        if (intHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Method to deal damage to other object's hp
    public int DealDamage() { return intDamage; }

    public int getHp() { return  intHp; }
    public int getMaxHp() { return intMaxHp; }
}
