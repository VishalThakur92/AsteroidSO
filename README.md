
​🧭 **`Index`**
- [**Singleton - A good design Pattern**](#singleton-design-pattern)
- [**Scalable Architecture**](#scalable-architecture)
- [**Tweak Game Settings**](#project-hierarchy)
- [**Tweak Player Settings**](#scene-hierarchy)
- [**Tweak Enemy Settings**](#scipts-structure)
- [**Tweak Powerup Settings**](#asset-bundles)
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
1.  Create a new Scriptable Object for new enemy via **`Create>Data>Powerup`**
2.  Fill in the parameters as desired
3.  All parameters are quite obvious, but one which is the **`CustomBehaviourPrefab`**
4.  **`CustomBehaviourPrefab`** is basically a prefab  that has a script on it which inherits from **`Powerup`** base class. This is done in such a manner because a new powerup may have custom behaviour. Hence  the **`OnCollected()`** can be overridden and custom algorithm can be added there. 
5.  Drag and drop this newly created scriptable object to the **`SpawnManager's`** **`Powerups`** parameter as specifed in the image above


