﻿using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Params
    //Scriptable Object Data
    [SerializeField]
    public GameDataSO gameplaySettings;

    [SerializeField]
    public PlayerDataSO playerData;

    //player ref
    public SpaceShip playerSpaceShip;


    public int score { get; private set; }

    //Cut scene Algo
    public bool cutSceneOver = false;

    public static GameManager Instance { get; private set; }


    #endregion

    #region Methods
    private void Awake()
    {
        //Singleton Logic
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        //Initialize Player spaceship as per the data specifed in the player data SO
        playerSpaceShip.Initalize(playerData.name, playerData.spaceShipSprite, playerData.maxPlayerHealth, playerData.acceleration, playerData.rotationSpeed);
    }

    //Cut scene Algo
    public void OnCutSceneOver()
    {
        if (!cutSceneOver)
        {
            cutSceneOver = true;
            NewGame(false);
        }
    }


    //Handle Restart
    private void Update()
    {
        if (playerSpaceShip.health <= 0 && Input.GetKeyDown(KeyCode.Return)) {
            NewGame(true);
        }
    }


    //Do stuff on new Game, basically reset stuff
    public void NewGame(bool respawnPlayer)
    {
        SpawnManager.Instance.Reset();

        SpawnManager.Instance.StartSpawning();

        //Hide Game over UI
        UIManager.Instance.ToggleGameOverUI(false);

        //Set Score to 0 as is a new game
        SetScore(0);


        //Reset Player health to max
        playerSpaceShip.health = playerData.maxPlayerHealth;

        if(respawnPlayer)
            Respawn();
    }


    //Respwan In the center so it looks nice n clean
    public void Respawn()
    {
        playerSpaceShip.transform.position = Vector3.zero;
        playerSpaceShip.gameObject.SetActive(true);
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        //Reward score to player upon hittig small asteroids only
        if (asteroid.size < 0.7f) {
            SetScore(score + 100); // small asteroid
        }
    }



    public void GameOver()
    {
        UIManager.Instance.ShowGameOverScore("Score : " + score);
        UIManager.Instance.ShowHealth("0");
        UIManager.Instance.ToggleGameOverUI(true);

    }

    private void SetScore(int score)
    {
        this.score = score;
        UIManager.Instance.ShowScore(score.ToString());
    }

    void SetPlayerHealth(int health) {
        UIManager.Instance.ShowHealth(health.ToString());
    }


    //------Callbacks-------
    public void OnSpaceShipDestroyed(SpaceShip spaceShip) {
        //Do Game Over behaviour
        GameOver();
    }

    public void OnSpaceShipDamaged(int health) {
        SetPlayerHealth(health);
    }

    #endregion
}
