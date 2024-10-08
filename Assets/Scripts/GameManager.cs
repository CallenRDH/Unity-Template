using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Camera mainCamera;
    public float spawnRate = 5f; // Rate of spawning in seconds, changeable in Inspector
    public int numberToSpawn = 1; // Number of prefabs to spawn at a time, changeable in Inspector

    void Start()
    {
        // Start calling the SpawnPrefab method at a rate specified in the Inspector
        InvokeRepeating("SpawnPrefab", 0f, spawnRate);
    }

    void Update()
    {
        // Check if there is an object with the "Player" tag
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("No object with 'Player' tag found. Stopping the game.");
            CancelInvoke("SpawnPrefab");
            StopGame();
        }
    }

    void SpawnPrefab()
    {
        if (prefabToSpawn != null && mainCamera != null)
        {
            for (int i = 0; i < numberToSpawn; i++)
            {
                // Get the forward direction of the main camera
                Vector3 spawnDirection = mainCamera.transform.forward;

                // Set the spawn position 50 units away from the camera in the forward direction
                Vector3 spawnPosition = mainCamera.transform.position + spawnDirection * 50f;

                // Set the y-coordinate to exactly 1
                spawnPosition.y = 1;

                // Instantiate the prefab at the calculated position
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogError("PrefabToSpawn or MainCamera is not assigned in the inspector!");
        }
    }

    void StopGame()
    {
        // Stops the game in the editor or quit the application in a build
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}