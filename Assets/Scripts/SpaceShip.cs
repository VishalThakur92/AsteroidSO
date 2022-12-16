using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : MonoBehaviour, IDamageable
{
    #region Params
    [SerializeField]
    public PlayerDataSO playerData;


    public new Rigidbody2D rigidbody { get; private set; }

    [SerializeField]
    Weapon weapon;

    //player' shield Ref
    GameObject myShield;

    //is player applying acceleration
    public bool thrusting { get; private set; }


    //direction to which the player shall turn
    public float turnDirection { get; private set; } = 0f;

    //player hit FX ref
    public ParticleSystem explosionEffect;

    //public float respawnDelay = 3f;
    public float respawnInvulnerability = 3f;

    //current health of player
    public int health;

    //is shield active right now ?
    public bool isShieldActive = false;

    //inital Cut scene has been completed, if true continues to main gameplay
    bool cutSceneAnimationComplete = false;

    #endregion


    #region Methods
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // Turn off collisions for a few seconds after spawning to ensure the
        // player has enough time to safely move away from asteroids
        gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        Invoke(nameof(TurnOnCollisions), respawnInvulnerability);
    }


    //Handle User Input 
    private void Update()
    {
        if (!cutSceneAnimationComplete)
            return;

        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {

            GameManager.Instance.OnCutSceneOver();
            turnDirection = 1f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {

            GameManager.Instance.OnCutSceneOver();
            turnDirection = -1f;
        } else {
            turnDirection = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameManager.Instance.OnCutSceneOver();
            weapon.Shoot();
        }
    }


    //Movement 
    private void FixedUpdate()
    {
        if (thrusting) {
            rigidbody.AddForce(transform.up * playerData.acceleration);
        }

        if (turnDirection != 0f) {
            rigidbody.AddTorque(playerData.rotationSpeed * turnDirection);
        }
    }



    //Blaster Shoot where Crecent Bullets are fired
    void BlasterShoot() {

        Bullet bullet1 = Instantiate(playerData.blasterBulletPrefab, transform.position, transform.rotation);

        bullet1.Project(transform.up);
    }

    private void TurnOnCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }



  
    //Start Blaster Mode Behaviour
    void InitializeBlasterMode() {
        StartCoroutine(BlasterModeBehaviour());
    }

    //Start Barrier or Shield Mode Behaviour
    void InitializeBarrierMode() {
        ToggleShield(true);
    }


    //Toggle Shield On or Off
    public void ToggleShield(bool flag) {
        if (flag)
        {
            if (myShield != null)
                Destroy(myShield);

            myShield = Instantiate(playerData.shieldPrefab, this.transform, false);
        }
        else {
            if(myShield != null)
                Destroy(myShield);
        }

        isShieldActive = flag;
    }


    //----Callbacks------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(collision.gameObject.GetComponent<Asteroid>().damagePoints);
        }
    }

    public void OnPlayerCutSceneAnimationComplete()
    {
        cutSceneAnimationComplete = true;
        GetComponent<Animator>().enabled = false;
    }

    public void OnPowerUpCollectedBehaviour(PowerUp powerUp)
    {
        switch (powerUp.powerUpData.powerUpType)
        {
            case Globals.Powerups.blaster:
                InitializeBlasterMode();
                break;
            case Globals.Powerups.barrier:
                InitializeBarrierMode();
                break;
        }
    }


    //-------Coroutines----------
    IEnumerator BlasterModeBehaviour()
    {
        //currentShootingMode = ShootingModes.blaster;
        InvokeRepeating(nameof(BlasterShoot) ,.1f , .1f);
        yield return new WaitForSeconds(10);
        CancelInvoke(nameof(BlasterShoot));
        //currentShootingMode = ShootingModes.normal;s
        StopCoroutine(BlasterModeBehaviour());
    }

    public void TakeDamage(int damage)
    {

        if (!isShieldActive)
        {
            health -= damage;

            if (health < 0)
                health = 0;

            GameManager.Instance.OnPlayerHitAsteroidBehaviour(this);
        }
        else
        {
            ToggleShield(false);
        }

    }
    #endregion

}
