
​🧭 **`Index`**
- [**Singleton - A good design Pattern**](#singleton-design-pattern)
- [**Scalable Architecture**](#scalable-architecture)
- [**Tweak Game Settings**](#tweak-game-settings)
- [**Tweak Player Settings**](#tweak-player-settings)
- [**Tweak Enemy Settings**](#tweak-enemy-settings)
- [**Tweak Powerup Settings**](#tweak-powerup-settings)
- [**How to Add new Enemy**](#add-a-new-enemy)
- [**How to Add new Powerup**](#add-a-new-powerup)


# Singleton design Pattern
![ton](https://user-images.githubusercontent.com/16806053/208237384-f937fd29-e53b-4f4a-b3c3-ca3208c0c3da.PNG)

The project follows Singleton Design pattern as well as follows SOLID Principles, and lays specific emphasis on a script to only have a single responsibility to avoid spaghetti code.

Breakdown -
1. **`GameManager.cs`** - Controls the core algorithm of the game
1. **`SpawnManager.cs`** - Spawns enemy(Asteroids) and powerups depending upon tweakable spawn algorithm
1. **`UIManager.cs`** - Handles UI
1. **`VFXManager.cs`** - Handles particle effects


# Scalable Architecture

The project utilises Interfaces and Abstract classes to keep things abstract which eventually helps the code base to be scalable.

Breakdown -
1. **`ICollectible`** - Anything than can be collected, All Powerups inherit from this interface. In Future if any object is added to the game which can be collected, then it can be derived from this interface.
1. **`IDamageable`** - Anything that can be damaged, Player(spaceship) and Enemy(Asteroid) inherit from this interface. In Future if any object is added to the game which can be damaged, then it can be derived from this interface.
1. **`Powerup Abstract Base Class`** - Base class for all powerups, A new powerup should inherit from this class to share already implemented similar functionalities.
1. **`Weapon Abstract Base Class`** -  Base class for all weapons, A new weapon should inherit from this class to share already implemented similar functionalities.


# Tweak Game Settings
![gameplay](https://user-images.githubusercontent.com/16806053/208238758-19693319-4b6a-46b7-be59-d9d553ba2220.PNG)

Breakdown -
1.  **`SpawnDistance`** - Distance between spawned objects(Enemies)
2.  **`AsteroidSpawnDelay`** - Delay in spawning an Enemy(Asteroid)
2.  **`AsteroidAmountPerSpawn`** -Total Enemies(Asteroids) spawned during one Spawn()
2.  **`PowerUpSpawnDelay`** - Delay in spawning an Powerup
2.  **`PowerUpAmountPerSpawn`** - Total powerups spawned during one Spawn()

# Tweak Player Settings
![player](https://user-images.githubusercontent.com/16806053/208238695-fc1077fb-a31a-4648-b54c-76942f39d01b.PNG)


Breakdown -
1.  **`spaceShipName`** - Name of this player's spaceship
2.  **`spaceShipSprite`** - Graphic of this player's spaceship
2.  **`maxPlayerHealth`** - Max player health
2.  **`acceleration`** - Speed with which this player's spaceship shall move
2.  **`rotationSpeed`** - Rate of rotation of player's spaceship


# Tweak Powerup Settings
![power](https://user-images.githubusercontent.com/16806053/208238316-1971c9ae-08e9-4881-86a9-947d7473bac3.PNG)

Breakdown -
1.  **`powerupName`** - Name of this powerup
2.  **`sprite`** - Graphic of this powerup
2.  **`customBehaviourPrefab`** - is basically a prefab  that has a script on it which inherits from **`Powerup`** base class. This is done in such a manner because a new powerup may have custom behaviour. Hence  the **`OnCollected()`** can be overridden and custom algorithm can be added there. 
2.  **`movementSpeed`** - Movement Speed of this powerup
2.  **`lifetime`** - Time for which this powerup shall stay alive in the scene
2.  **`duration`** - Time for whch this powerup is effectve(once collected)




# Tweak Enemy Settings
![enemy](https://user-images.githubusercontent.com/16806053/208238457-5cc1bfa1-420b-460f-aaf0-ef556a09c56b.PNG)


Breakdown -
1.  **`enemyName`** - Name of this enemy
2.  **`enemySprite`** - Graphic of this enemy
2.  **`size`** - Default spawn size
2.  **`minSize`** - Min size  of this enemy as used by splitting and  spawning algorithm
2.  **`maxSize`** - Max size  of this enemy as used by splitting and  spawning algorithm
2.  **`movementSpeed`** - Speed  of this Asteroid's Trajectory
2.  **`maxLifetime`** - Time for which this enemy shall stay alive in the scene
2.  **`damagePoints`** - Damage that this enemy will cause to the player



# Add a new Enemy
![newEnemy](https://user-images.githubusercontent.com/16806053/208237940-a699b7b6-9d63-43ad-a991-2ca8d071f141.PNG)

Unless the new enemy is expected to behave in a custom way, it is quite simple to add a new enemy to the game.

Breakdown -
1.  Create a new Scriptable Object for new enemy via **`Create>Data>Enemy`**
2.  Fill in the parameters as desired
3.  Drag and drop this newly created scriptable object to the **`SpawnManager's`** **`Enemies`** parameter as specifed in the image above


# Add a new Powerup
![newPower](https://user-images.githubusercontent.com/16806053/208238080-c8ef2ddc-5325-4016-968c-72d6befc8461.PNG)

Breakdown -
1.  Create a new Scriptable Object for new Powerup via **`Create>Data>Powerup`**
2.  Fill in the parameters as desired
3.  All parameters are quite obvious, but one which is subtle, which is the **`CustomBehaviourPrefab`**
4.  **`CustomBehaviourPrefab`** is basically a prefab  that has a script on it which inherits from **`Powerup`** base class. This is done in such a manner because a new powerup may have custom behaviour. Hence  the **`OnCollected()`** can be overridden and custom algorithm can be added there. 
5.  Drag and drop this newly created scriptable object to the **`SpawnManager's`** **`Powerups`** parameter as specifed in the image above


