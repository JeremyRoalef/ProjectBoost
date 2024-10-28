using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * The asteroid will move based on where it spawns. If it spawns on the left side of the gamespace, it will move to the right. If it moves
 * on the right side of the gamespace, it will move left. If it spawns above the gamespace, it will move down.
 * 
 * The asteroid will rotate based on a random number. Expected problem: rotating the object will cause the movement to act weird if using
 * relative velocity vectors. Potential solution: use real-world vector velocity.
 * 
 */
public class AsteroidMovement : MonoBehaviour
{
    //Cashe References
    Rigidbody asteroidRb;
    
    //Attributes
    Vector3 asteroidVelocity = Vector3.zero;
    Vector3 asteroidRotation;

    float fltMinRotation = 0.5f;
    float fltMaxRotation = 2.0f;

    float fltMinYSpeed = 5f;
    float fltMaxYSpeed = 9f;

    float fltMinXSpeed = 5f;
    float fltMaxXSpeed = 9f;

    void Awake()
    {
        asteroidRb = GetComponent<Rigidbody>(); //Get rigidbody here. It fixed problems somehow
    }
    void Start()
    {
        InitializeAsteroid();
    }

    private void InitializeAsteroid()
    {
        //Set asteroid rotation Vector
        float fltXRotation = GenerateRandomNumber(fltMinRotation, fltMaxRotation);
        float fltYRotation = GenerateRandomNumber(fltMinRotation, fltMaxRotation);
        float fltZRotation = GenerateRandomNumber(fltMinRotation, fltMaxRotation);
        asteroidRotation = new Vector3(fltXRotation, fltYRotation, fltZRotation) * Time.deltaTime;
    }

    void Update()
    {
        //rotate asteroid
        transform.Rotate(asteroidRotation);
    }


    //Method to set the velocity direction of the asteroid
    public void SetAsteroidVelocity(Vector3 velocityVector)
    {
        asteroidRb.velocity += velocityVector;
    }

    //Methods to set velocity based on the direction given
    public void SetAsteroidVelocityDown()
    {
        float fltDownSpeed = GenerateRandomNumber(-fltMaxYSpeed, -fltMinYSpeed); //Generate speed to go down
        SetAsteroidVelocity(new Vector3(0, fltDownSpeed, 0));
    }

    public void SetAsteroidVelocityLeft()
    {
        float fltLeftSpeed = GenerateRandomNumber(-fltMaxXSpeed, -fltMinXSpeed); //Generate speed to go left
        SetAsteroidVelocity(new Vector3(fltLeftSpeed, 0, 0));
    }

    public void SetAsteroidVelocityRight()
    {
        float fltRightSpeed = GenerateRandomNumber(fltMinXSpeed, fltMaxXSpeed); //Generate speed to go right
        SetAsteroidVelocity(new Vector3(fltRightSpeed, 0, 0));
    }

    public void SetAsteroidVelocityUp()
    {
        float fltUpSpeed = GenerateRandomNumber(fltMinYSpeed, fltMaxYSpeed); //Generate speed to go up
        SetAsteroidVelocity(new Vector3(0, fltUpSpeed, 0));
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
