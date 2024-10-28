using System.Collections;
using System.Collections.Generic;
using UnityEngine; //the namespace all the monobehavior content exists

/*
->Class Movement INHERITS MonoBehavior (MonoBehavior properties & methods are extended to Movement class)
->Classes should account for one type of behavior. Ex: a movement class should account for the movement
  of the object it is attached to. Collision class should account for the collision detection on an object
->Use encapsulation in code. (Getters & Setters, private attributes, etc.)
 */

/*
 * This script is responsible for everthing the player does while moving. Below are all the things that this script controls:
 * 
 * 1) Moving player vertically & horizontally. The player will move vertically by applying a relative force upward to the player's rigidbody when
 * the space key is pressed and held down. The player will move horizontally by applying a relative force leftward/rightward to the player's
 * rigidbody when the a/d key is pressed and held down.
 * 
 * 2) Audio & Particle systems. When the player move, the respective audio and particles will play.
 * 
 * 3) Updating game UI for distance traveled & remaining fuel.
 * 
 */
public class Movement : MonoBehaviour 
{
    //Create serialized fields
    [SerializeField] float fltVerticalSpeed = 1f;
    [SerializeField] float fltHorizontalSpeed = 1f;
    [SerializeField] float fltMaxFuelTime;
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    //Cashe References
    Rigidbody playerRb;
    AudioSource audioSource;
    GameObject gameUI;

    //Attributes
    float fltRemainingFuelTime;

    void Awake()
    {
        gameUI = GameObject.FindGameObjectWithTag("UI");
        fltRemainingFuelTime = fltMaxFuelTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //Get rb attached to the same object the script is on
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayerVertically(); //Get the input from the user
        MovePlayerHorizontally(); //Get the input from the user
        UpdateDistanceTraveledText(); //Update the distance traveled text in the gameUI
        UpdateFuelImage(); //Update the fuel image in the gameUI
    }

    void UpdateFuelImage()
    {
        float fltRemainingFuelProportion = fltRemainingFuelTime/fltMaxFuelTime;
        gameUI.GetComponent<GameSceneUIHandler>().UpdateFuelImage(fltRemainingFuelProportion);
    }

    private void UpdateDistanceTraveledText()
    {
        float fltDistanceTraveled = transform.position.y;
        gameUI.GetComponent<GameSceneUIHandler>().UpdateDistanceTraveled(fltDistanceTraveled);
    }

    //Method to determine if player should rotate
    void MovePlayerHorizontally()
    {
        //If player is pressing both rotate buttons, don't rotate
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            PlayLeftThrustParticles();
            PlayRightThrustParticles();
        }
        //Otherwise, if they're pressing A, move player to the left
        else if (Input.GetKey(KeyCode.A))
        {
            playerRb.AddRelativeForce(new Vector3(-fltHorizontalSpeed, 0, 0));
            PlayRightThrustParticles();
        }
        //Otherwise, if they're pressing D, move player to the right
        else if (Input.GetKey(KeyCode.D))
        {
            playerRb.AddRelativeForce(new Vector3(fltHorizontalSpeed, 0, 0));
            PlayLeftThrustParticles();
        }
        else
        {
            StopLeftThrustParticles();
            StopRightThrustParticles();
        }
    }

    //Method to thrust the player
    void MovePlayerVertically()
    {
        if (Input.GetKey(KeyCode.W)) //Set KeyCode enumeration type to Space
        {
            fltRemainingFuelTime -= Time.deltaTime;
            if (fltRemainingFuelTime <= 0)
            {
                StopThrustingSequence();
                return;
            }
            StartThrustingSequence();
        }
        else
        {
            StopThrustingSequence();
        }
    }

    //Method to run when player starts thrusting
    void StartThrustingSequence()
    {
        //F = ma, meaning a = F/m. Acceleration of object from force is dependent on amount of force & object's mass

        Vector3 forceAmount = Vector3.up * fltVerticalSpeed * Time.deltaTime; //Vector3.up is the object's relative up direction
        playerRb.AddRelativeForce(forceAmount); //Add force to rb relative to its own direction (not world space)

        //Play the thrust sfx
        PlayThrustSound();

        //Play main thrust particles
        PlayMainThrustParticles();
    }

    //Method to run when player stops thrusting
    void StopThrustingSequence()
    {
        //Stop the thrust sfx
        StopThrustSound();

        //Stop main thrust particles
        StopMainThrustParticles();
    }

    //Method to turn the thrust sfx on
    void PlayThrustSound()
    {
        //If the thrust sfx is not playing, play the sfx
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSFX);
        }
    }
    
    //Method to turn the thrust off
    void StopThrustSound()
    {
        audioSource.Stop();
    }

    //Method to play thrust particles
    void PlayMainThrustParticles()
    {
        //Play thrust particles
        if (!mainThrustParticles.isEmitting)
        {
            mainThrustParticles.Play();
        }
    }

    //Method to stop thrust particles
    void StopMainThrustParticles()
    {
        mainThrustParticles.Stop();
    }

    //Method to play left thrust particles
    void PlayLeftThrustParticles()
    {
        if (!leftThrustParticles.isEmitting)
        {
            leftThrustParticles.Play();
        }
    }

    //Method to stop left thrust particles
    void StopLeftThrustParticles()
    {
        leftThrustParticles.Stop();
    }

    //Method to play right thrust particles
    void PlayRightThrustParticles()
    {
        if (!rightThrustParticles.isEmitting)
        {
            rightThrustParticles.Play();
        }
    }

    //Method to stop right thrust particles
    void StopRightThrustParticles()
    {
        rightThrustParticles.Stop();
    }
}
