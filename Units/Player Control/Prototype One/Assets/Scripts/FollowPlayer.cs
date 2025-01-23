using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    public GameObject player;
    private Vector3 offset = new Vector3(0f, 7.94f, -21.26f); // Don't forget to explicitly make the literals float to avoid compiling error
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {   // Offset the camera behind the player by adding to the player's position
        transform.position = player.transform.position + offset;
    }
}
