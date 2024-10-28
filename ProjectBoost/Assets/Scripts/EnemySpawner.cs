using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enemies will spawn based on the TopEnemySpawner, LeftEnemySpawner, and RightEnemySpawner locations.
 * Their position will be determined based on the position of each respective object, randomly set more or less than the actual position
 * of the object locations. All spawining should be done off screnn.
 * 
 * Spawn Logic: To start, spawn objects every 3 seconds. If an object spawns on the left object spawner, make sure it moves to the right.
 * If an object spawns on the right object spawner, make sure it moves to the left. If an object spawns on the top object spawner, depending
 * if it spawned left or right of the acutal position of the top spawn object, move it towards the middle of the screen.
 * 
 * Future additions can randomize time it takes to spawn an object, preferring to spawn stronger and harder enemies, or even spawning waves of
 * enemies based on preset conditions.
 */

public class EnemySpawner : MonoBehaviour
{
    //Serialized fields
    [SerializeField] GameObject leftEnemySpawner, rightEnemySpawner, topEnemySpawner;
    [SerializeField] GameObject bigAsteroid;
    //[SerializeField] GameObject enemyType1, enemyType2, enemyType3;

    //Attributes
    bool boolIsSpawningEnemies = false;
    float fltWaitToSpawnEnemies = 1f;

    float fltTopSpawnInterval = 35f; //The x distance from the top enemy spawner the object can spawn away from
    float fltSideSpawnInterval = 30f; //The y distance from the left/right enemy spawner the object can spawn away from
    
    void Update()
    {
        //If enemies are not spawning, spawn them
        if (!boolIsSpawningEnemies)
        {
            StartCoroutine(SpawnEnemies(fltWaitToSpawnEnemies));
        }
    }

    IEnumerator SpawnEnemies(float fltWaitTime)
    {
        boolIsSpawningEnemies = true;

        //Generate a random integer between 0, 1, and 2. switch the spawn location to top spawn (0), left spawn (1), or right spawn (2)
        //based on the number
        int intSpawnPosition = GenerateRandomNumber(0,3); //3 is excludeded

        //Debug.Log("spawn number = " + intSpawnPosition);

        switch (intSpawnPosition)
        {
            case 0:
                StartSpawnTopSequence();
                break;
            case 1:
                StartSpawnLeftSequence();
                break;
            case 2:
                StartSpawnRightSequence();
                break;
        }

        yield return new WaitForSeconds(fltWaitTime);
        boolIsSpawningEnemies = false;
    }

    void StartSpawnTopSequence()
    {
        //Debug.Log("top spawn sequence started");

        //Create a spawn position for the object
        float fltChangeInPosition = GenerateRandomNumber(-fltTopSpawnInterval, fltTopSpawnInterval); //the change from the top spawn location to new spawn location

        Vector3 spawnPos = topEnemySpawner.transform.position; //The top spawn location
        //Debug.Log("Current spawn position: " + spawnPos);

        spawnPos.x += fltChangeInPosition; //Change the spawn location
        //Debug.Log("New spawn position: " + spawnPos);

        //Create asteroid & save in temp var
        GameObject asteroid = Instantiate(bigAsteroid, spawnPos, Quaternion.identity);

        //Set asteroid movement to be going down & towards the center

        if (fltChangeInPosition > 0) //If it spawns on the right, move it left
        {
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityDown();
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityLeft();
        }
        else //Otherwise, move it right
        {
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityDown();
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityRight();
        }

        //Debug.Log("Asteroid velocity: " + asteroid.GetComponent<Rigidbody>().velocity);
    }
    void StartSpawnLeftSequence()
    {
        //Debug.Log("left spawn sequence started");

        //Create a spawn position for the object
        float fltChangeInPosition = GenerateRandomNumber(-fltSideSpawnInterval, fltSideSpawnInterval); //the change from the left spawn location to new spawn location

        Vector3 spawnPos = leftEnemySpawner.transform.position; //The left spawn location
        //Debug.Log("Current spawn position: " + spawnPos);

        spawnPos.y += fltChangeInPosition; //Change the spawn location
        //Debug.Log("New spawn position: " + spawnPos);

        //Create asteroid & save in temp var
        GameObject asteroid = Instantiate(bigAsteroid, spawnPos, Quaternion.identity);

        //Set asteroid movement to be going right & towards the center

        if (fltChangeInPosition > 0) //If it spawns upward, move it down
        {
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityDown();
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityRight();
        }
        else //Otherwise, move it up
        {
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityUp();
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityRight();
        }

        //Debug.Log("Asteroid velocity: " + asteroid.GetComponent<Rigidbody>().velocity);
    }
    void StartSpawnRightSequence()
    {
        //Debug.Log("right spawn sequence started");

        //Create a spawn position for the object
        float fltChangeInPosition = GenerateRandomNumber(-fltSideSpawnInterval, fltSideSpawnInterval); //the change from the left spawn location to new spawn location

        Vector3 spawnPos = rightEnemySpawner.transform.position; //The left spawn location
        //Debug.Log("Current spawn position: " + spawnPos);

        spawnPos.y += fltChangeInPosition; //Change the spawn location
        //Debug.Log("New spawn position: " + spawnPos);

        //Create asteroid & save in temp var
        GameObject asteroid = Instantiate(bigAsteroid, spawnPos, Quaternion.identity);

        //Set asteroid movement to be going right & towards the center

        if (fltChangeInPosition > 0) //If it spawns upward, move it down
        {
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityDown();
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityLeft();
        }
        else //Otherwise, move it up
        {
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityUp();
            asteroid.GetComponent<AsteroidMovement>().SetAsteroidVelocityLeft();
        }

        //Debug.Log("Asteroid velocity: " + asteroid.GetComponent<Rigidbody>().velocity);
    }

    //methods to generate a random number. Im sick of typing out random.range!
    int GenerateRandomNumber(int min, int max)
    {
        int randomNum = Random.Range(min, max);
        return randomNum;
    }
    float GenerateRandomNumber(float min, float max)
    {
        float randomNum = Random.Range(min, max);
        return randomNum;
    }
}
