using UnityEngine;

public class GameSpaceMover : MonoBehaviour
{
    Rigidbody cameraRb;

    // Start is called before the first frame update
    void Start()
    {
        cameraRb = GetComponentInParent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        //If player enters the collider, move the object up at player's velocity
        if (other.gameObject.tag == "Player" && other.GetComponent<Rigidbody>().velocity.y > 0)
        {
            float fltPlayerVelocity = other.GetComponent<Rigidbody>().velocity.y;
            MoveUp(fltPlayerVelocity);
        }
    }

    void MoveUp(float fltVelocity)
    {
        cameraRb.velocity = new Vector3(0, fltVelocity, 0);
    }
    void OnTriggerStay(Collider other)
    {
        //If player stays in the collider & is not moving down, move the object up at player's velocity
        if (other.gameObject.tag == "Player" && other.GetComponent<Rigidbody>().velocity.y > 0)
        {
            float fltPlayerVelocity = other.GetComponent<Rigidbody>().velocity.y;
            MoveUp(fltPlayerVelocity);
        }
    }
    void OnTriggerExit(Collider other)
    {
        //If player leaves the collision, stop moving object
        if (other.gameObject.tag == "Player")
        {
            StopMoving();
        }
    }
    void StopMoving()
    {
        cameraRb.velocity = Vector3.zero;
    }
}
