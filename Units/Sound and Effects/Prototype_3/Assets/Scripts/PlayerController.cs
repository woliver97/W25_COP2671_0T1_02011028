using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
public float jumpForce = 10;
public float gravityModifier;
public bool isOnGround = true;

        void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        // isonground variable so the player cant double jump
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce , ForceMode.Impulse);
            isOnGround = false;
        }
       // transform.Translate(Vector3.forward * Time.deltaTime * 8);
    }
    
}
