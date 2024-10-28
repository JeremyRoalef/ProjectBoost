using UnityEngine;

/*
 * This class will allow the creation of scriptable objects for the purpose of implementing a health system. Each scriptable object will
 * store health and damage values for the objects to use.
 */


[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Object")]
public class ObjectScriptableObject : ScriptableObject
{
    public int health = 100;
    public int damage = 10;
    public AudioClip deathSound;
}
