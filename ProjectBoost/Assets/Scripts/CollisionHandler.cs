using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Serialized fields
    [SerializeField] float fltReloadDelay = 1f;
    [SerializeField] float fltNextLevelDelay = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] GameObject gameUI;

    //Cashe references
    AudioSource audioSource;
    HealthSystem playerHealthSystem;

    //private attributes
    bool boolIsTransitioning = false;

    void Awake()
    {
        gameUI = GameObject.FindGameObjectWithTag("UI");
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerHealthSystem = GetComponent<HealthSystem>();
    }

    void OnCollisionEnter(Collision other)
    {
        /*
         * Problem with collision: Unity collision tags do not work as expected when it comes to parent and child objects. If a parent
         * object's tag is tagged "parent" and a child object tag is tagged "child", when the player collides with the child object, the
         * parent object's tag is returned.
         * 
         * Solution to collision: Change the collider to a trigger. For the Killbox collision, this will work. However, if I wanted to check
         * if the player collides with the wall, let's say, I would not want to use a trigger because that will allow the player to phase
         * through the wall. This would require using the ContactPoint class to get the tag of the real object responsible for the collision,
         * not just the parent object.
         * 
         * Credit for idea - ChatGPT
         */

        //Debug.Log("Collision occurred");
        //Debug.Log("Tag of object collision: " + other.gameObject.tag);
        switch(other.gameObject.tag) {
            case "Dangerous":
                //get the object's damage & reduce player's health
                int intEnemyDamage = other.gameObject.GetComponent<HealthSystem>().DealDamage();
                TakeDamageSequence(intEnemyDamage);

                //Get the player's collision damage & reduce enemy's health
                int intPlayerDamage = playerHealthSystem.DealDamage();
                other.gameObject.GetComponent<HealthSystem>().TakeDamage(intPlayerDamage);
                //Debug.Log("Enemy HP: " + other.gameObject.GetComponent<HealthSystem>().getHp());

                break;
            case "Fuel":
                RefuelPlayer();
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                //Do nothing
                break;
        }
    }

    private void TakeDamageSequence(int intEnemyDamage)
    {
        //Apply damage to player's health
        if (playerHealthSystem.TakeDamage(intEnemyDamage))
        {
            StartDeathSequence();
        }

        //cast integers of player hp to a float
        float fltCurrentHp = (float)playerHealthSystem.getHp();
        float fltMaxHp = (float)playerHealthSystem.getMaxHp();

        //get proportion of remaining hp
        float fltHealthProportion = fltCurrentHp / fltMaxHp;

        //update health image
        gameUI.GetComponent<GameSceneUIHandler>().UpdateHealthImage(fltHealthProportion);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger entered");
        //Debug.Log("Tag of trigger collider: " + other.gameObject.tag);

        switch (other.gameObject.tag)
        {
            case "Killbox":
                //Debug.Log("Killing player");
                Destroy(gameObject);
                break;
            default:
                //Debug.Log("Tag didnt match any current tag");
                break;
        }
    }

    //Method to run when player crashes into dangerous obstacles
    void StartDeathSequence()
    {
        if (boolIsTransitioning) {return;}


        boolIsTransitioning = true;
        audioSource.Stop();
        PlayCrashParticles();
        GetComponent<Movement>().enabled = false; //Disable player movement ability
        PlayCrashSound();
        ReloadScene();
        KillPlayer();
    }

    //Method to run when the player finishes the level
    void StartFinishSequence()
    {
        if (boolIsTransitioning) {return;}

        boolIsTransitioning = true;
        audioSource.Stop();
        PlaySuccessParticles();
        GetComponent<Movement>().enabled = false; //Disable player movement ability
        PlaySuccessSound();
        LoadNextScene();
    }

    //Method to kill player
    void KillPlayer()
    {
        Debug.Log("Collision should kill player");
    }

    //Method to refuel player's fuel value
    void RefuelPlayer()
    {
        Debug.Log("Collision should refuel player");
    }

    //Method to load the next scene
    void LoadNextScene()
    {
        Debug.Log("Collision should load next scene");
        StartCoroutine(WaitToLoadNextScene());
    }

    //Method to load the current scene
    void ReloadScene()
    {
        //Start the coroutine to reload the current scene
        StartCoroutine(WaitToReloadScene());
    }

    //Method to play the crash sfx
    void PlayCrashSound()
    {
        audioSource.PlayOneShot(crashSFX);
    }

    //Method to play the success sfx
    void PlaySuccessSound()
    {
        audioSource.PlayOneShot(successSFX);
    }

    void PlayCrashParticles()
    {
        crashParticles.Play();
    }

    void PlaySuccessParticles()
    {
        successParticles.Play();
    }

    //IEnumerator to wait to reload the current scene
    IEnumerator WaitToReloadScene()
    {
        int intCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //Wait to reload current scene
        yield return new WaitForSeconds(fltReloadDelay);
        //Reload the current scene
        SceneManager.LoadScene(intCurrentSceneIndex);
    }

    //IEnumerator to wait to load the next scene
    IEnumerator WaitToLoadNextScene()
    {
        int intNextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        //Wait to load next scene
        yield return new WaitForSeconds(fltNextLevelDelay);

        //Determine if there is a next scene in build
        bool boolThereIsNextScene = intNextSceneIndex < SceneManager.sceneCountInBuildSettings;

        //If there's a next scene in build, load it
        if (boolThereIsNextScene)
        {
            SceneManager.LoadScene(intNextSceneIndex);
        }
        //Otherwise, restart game (scene at index 0)
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
