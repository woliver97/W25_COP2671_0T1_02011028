using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerup = false;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
       
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        //offset in the end so the powerup is in the ground instead of in the player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Powerup")) {
            Destroy(other.gameObject);
            hasPowerup = true;
            // StartCouroutine is a built-in Unity method that allows us to start a coroutine, which is a function that can run independently of the Update method
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }                                       }
    IEnumerator PowerupCountdownRoutine() {
        // Yield is used for enabling us to run this timer in a place outside of the Update method
        // WaitForSeconds is a built-in Unity class that waits for a specified amount of time
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("enemy") && hasPowerup) {
            
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            
            // Enemy position minus player position gives the vector from player to enemy
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            
            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}

