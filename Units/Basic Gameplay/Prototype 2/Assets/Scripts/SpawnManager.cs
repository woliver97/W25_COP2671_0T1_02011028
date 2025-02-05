using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   public GameObject[] animalPrefab;
   private float spawnRangeX = 20;
   private float spawnPosZ = 30;
   private float startDelay = 2;
   private float spawnInterval = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }
    void SpawnRandomAnimal()
        { 
            int animalIndex = Random.Range(0, animalPrefab.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
            Instantiate(animalPrefab[animalIndex], spawnPos , animalPrefab[animalIndex].transform.rotation);
        }
    
    void Update()
    {
     /*  if (Input.GetKeyDown(KeyCode.S)) {
           SpawnRandomAnimal();}
      */ 
        
    }
}
