using System.Collections;
using UnityEngine;

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

        InitializeGameStartBehaviour();
    }

    //Cut scene Algo
    public void InitializeGameStartBehaviour()
    {
        StartCoroutine(GameStartBehaviour());
    }


    IEnumerator GameStartBehaviour() {

        //Disable Player Spaceship Control
        playerSpaceShip.ToggleCanControl(false);

        //Show Cut Scene
        MontageManager.Instance.PlayGameStartCutScene();

        //Wait for player to acknowledge the Controls
        yield return new WaitUntil(() => HasPlayerAcknowledgedControls());

        //End Cut Scene
        MontageManager.Instance.EndGameStartCutScene();

        //Start game once complete - False because this is not a respawn but a fresh new game
        NewGame(false);
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
        //Donot bother with Canvas if this is a respawn and not the inital new game
        if (!respawnPlayer)
        {
            UIManager.Instance.ToggleCanvas(true);
            UIManager.Instance.ToggleShootGuideText(false);
        }

        //Reset Spawn Manager and Start Spawning again
        SpawnManager.Instance.Reset();
        SpawnManager.Instance.StartSpawning();

        //Hide Game over UI
        UIManager.Instance.ToggleGameOverUI(false);

        //Set Score to 0 and health to max health , as is a new game
        SetScore(0);
        SetPlayerHealth(playerData.maxPlayerHealth);

        //Enable player Controls
        playerSpaceShip.ToggleCanControl(true);

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

        //Disable player Controls
        playerSpaceShip.ToggleCanControl(false);

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
        //Reset Player health to max
        playerSpaceShip.health = playerData.maxPlayerHealth;
    }


    bool HasPlayerAcknowledgedControls() {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space))
            return true;
        else
            return false;
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
