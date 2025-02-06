using UnityEngine;

public class PlayerController : MonoBehaviour
{
private Rigidbody playerRb;
public float jumpForce = 10;
public float gravityModifier;
public bool isOnGround = true;

        void Start()
    {
       // Get component to get rigidbody. Transform didnt need that because every object
       // has the transform component by default, versus rigidbody which is optional
       // and you have to add to your gameObject if you need to use it 
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        // isonground variable so the player cant double jump
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            // Add force to the playerRb jump , ForceMode is the method of the force
            // which has 4 options: Force, Acceleration, Impulse, VelocityChange
            // Impulse is the best for jump because it gives a instant force
            playerRb.AddForce(Vector3.up * jumpForce , ForceMode.Impulse);
            isOnGround = false;
        }
       // transform.Translate(Vector3.forward * Time.deltaTime * 8);
    }
    private void OnCollisionEnter(Collision collision)
    {
        {
            isOnGround = true;
        }
    }
}
