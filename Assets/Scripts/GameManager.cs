using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Params
    //Scriptable Object Data
    [SerializeField]
    public GameDataSO gameData;

    //player ref
    public SpaceShip playerSpaceShip;

    [SerializeField]
    VFXManager vfxManager;

    //Game over UI
    public GameObject gameOverUI;

    public int score { get; private set; }
    public Text scoreText;

    //public int lives { get; private set; }
    public Text livesText;

    public Text healthText;
    public Text GameOverScoreText;

    //Cut scene Algo
    public bool cutSceneOver = false;

    public static GameManager Instance { get; private set; }


    #endregion

    #region Methods
    private void Awake()
    {
        //Singelton Logic
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
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

        gameOverUI.SetActive(false);
        playerSpaceShip.ToggleShield(false);

        SetScore(0);

        SetPlayerHealth(playerSpaceShip.playerData.maxPlayerHealth);

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
        GameOverScoreText.text = "Score : " + score;
        healthText.text = "0";
        gameOverUI.SetActive(true);

    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    void SetPlayerHealth(int health) {
        healthText.text = health.ToString();
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
