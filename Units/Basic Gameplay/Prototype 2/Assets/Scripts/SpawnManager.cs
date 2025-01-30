using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   public GameObject[] animalPrefab;
   private float spawnRangeX = 20;
   private float spawnPosZ = 20;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Range will select a random number between 0 and the length of the array
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
            int animalIndex = Random.Range(0, animalPrefab.Length);
           Instantiate(animalPrefab[animalIndex], spawnPos , animalPrefab[animalIndex].transform.rotation);
        }
    }
}
