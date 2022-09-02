using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    #region Params
    public static Spawner Instance { get; private set; }

    //All Asteroids and Powerup prefabs ever spawned
    List<Asteroid> spawnedAsteroids = new List<Asteroid>();
    List<PowerUp> spawnedPowerups = new List<PowerUp>();


    //This hits us and we get hurt :(
    public Asteroid asteroidPrefab;

    //All possible Powerups that will be spawned in the game
    public List<PowerUp> powerUpPrefabs = new List<PowerUp>();

    //Can be added to Game Data to expose one more param to configure game difficulty
    [Range(0f, 45f)]
    public float trajectoryVariance = 15f;


    //Game Data ref
    GameDataSO gameData;
    #endregion


    #region Methods
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gameData = GameManager.Instance.gameData;
    }


    //Start Spawning Asteroids and Powerups
    public void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnAsteroids), gameData.AsteroidSpawnDelay, gameData.AsteroidSpawnDelay);

        InvokeRepeating(nameof(SpawnPowerUps), gameData.PowerUpSpawnDelay, gameData.PowerUpSpawnDelay);
    }


    //Asteroid Spawning Logic
    public void SpawnAsteroids()
    {
        for (int i = 0; i < gameData.AsteroidAmountPerSpawn; i++)
        {
            // Choose a random direction from the center of the spawner and
            // spawn the asteroid a distance away
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * gameData.SpawnDistance;

            // Offset the spawn point by the position of the spawner so its
            // relative to the spawner location
            spawnPoint += transform.position;

            // Calculate a random variance in the asteroid's rotation which will
            // cause its trajectory to change
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Create the new asteroid by cloning the prefab and set a random
            // size within the range
            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            // Set the trajectory to move in the direction of the spawner
            Vector2 trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(trajectory);
            spawnedAsteroids.Add(asteroid);
        }
    }



    //Powerups Spawning Logic
    public void SpawnPowerUps()
    {
        for (int i = 0; i < gameData.PowerUpAmountPerSpawn; i++)
        {
            // Choose a random direction from the center of the spawner and
            // spawn the asteroid a distance away
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * gameData.SpawnDistance;

            // Offset the spawn point by the position of the spawner so its
            // relative to the spawner location
            spawnPoint += transform.position;

            // Calculate a random variance in the asteroid's rotation which will
            // cause its trajectory to change
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // size within the range
            int randomPowerUpIndex = Random.Range(0, powerUpPrefabs.Count);
            PowerUp powerUp = Instantiate(powerUpPrefabs[randomPowerUpIndex], spawnPoint, new Quaternion(0, 0, 0, 0));

            // Set the trajectory to move in the direction of the spawner
            Vector2 trajectory = rotation * -spawnDirection;
            powerUp.SetTrajectory(trajectory);

            spawnedPowerups.Add(powerUp);
        }
    }



    //On New Game we clean up old data, old asteroids and powerups
    //A new Begining..
    public void Reset()
    {
        CancelInvoke(nameof(SpawnAsteroids));
        CancelInvoke(nameof(SpawnPowerUps));

        //Remove all Asteroids and Powerups
        for (int i = 0; i < spawnedAsteroids.Count; i++)
        {
            if (spawnedAsteroids[i] != null)
                Destroy(spawnedAsteroids[i].gameObject);
        }

        spawnedAsteroids.Clear();

        for (int i = 0; i < spawnedPowerups.Count; i++)
        {
            if (spawnedPowerups[i] != null)
                Destroy(spawnedPowerups[i].gameObject);
        }

        spawnedPowerups.Clear();


    }
    #endregion
}
