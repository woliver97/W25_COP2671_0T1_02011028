using UnityEngine;

public class PlayerController : MonoBehaviour
{
// First create the var in the class
private Animator playerAnim;
private Rigidbody playerRb;
// Don't forget that with particles you still have to add them to the Player object in the Unity Editor
public ParticleSystem explosionParticle;
public ParticleSystem dirtParticle;
public float jumpForce = 10;
public float gravityModifier;
public bool isOnGround = true;
public bool gameOver;   
        void Start()
    {
       playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        // Then use the GetComponent method to get the component you're trying to access
        playerAnim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameOver != true) // or !gameOver
        {
            // Add force to the playerRb jump , ForceMode is the method of the force
            // which has 4 options: Force, Acceleration, Impulse, VelocityChange
            // Impulse is the best for jump because it gives a instant force
            playerRb.AddForce(Vector3.up * jumpForce , ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
        }
       // transform.Translate(Vector3.forward * Time.deltaTime * 8);
    }
    private void OnCollisionEnter(Collision collision)
    {
        {
            
            if (collision.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
                dirtParticle.Play();
                
            } else if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Game Over!");
                gameOver = true;
                // Sets Death_b to true 
                playerAnim.SetBool("Death_b", true);
                // Notice how the Set is set to the variable type . Also, don't forget to put the name in quotes
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
                dirtParticle.Stop();
            }
        }
    }
}
