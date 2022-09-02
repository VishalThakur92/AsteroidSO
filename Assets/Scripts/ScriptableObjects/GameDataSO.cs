using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameData")]
public class GameDataSO : ScriptableObject
{

    //Spawn spawn Config
    public float SpawnDistance = 12f;//Distance between Spawned Asteroids
    public float AsteroidSpawnDelay = 1f;//After 1 second
    public int AsteroidAmountPerSpawn = 1;//Spawn 1 Asteroid

    //powerup Spawn Config
    public float PowerUpSpawnDelay = 15f;//After 15 seconds
    public int PowerUpAmountPerSpawn = 1;//Spawn 1 powerUp


}
