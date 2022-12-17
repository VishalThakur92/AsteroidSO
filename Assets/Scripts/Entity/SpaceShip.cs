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
    Weapon defaultweapon;

    Weapon specialWeapon;

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
            //TODO remove this
            GameManager.Instance.OnCutSceneOver();
            defaultweapon.Shoot();
        }
    }


    //Movement 
    void FixedUpdate()
    {
        if (thrusting) {
            rigidbody.AddForce(transform.up * playerData.acceleration);
        }

        if (turnDirection != 0f) {
            rigidbody.AddTorque(playerData.rotationSpeed * turnDirection);
        }
    }



    private void TurnOnCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
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



    public void SetSpecialWeapon(Weapon weapon , float duration)
    {
        //Discard special weapon if already equiped
        DiscardSpecialWeapon();

        //Load and apply Special Weapon
        specialWeapon = Instantiate(weapon, transform, false);
        Invoke("DiscardSpecialWeapon", duration);
    }

    public void DiscardSpecialWeapon()
    {
        if (specialWeapon)
        {
            CancelInvoke("DiscardSpecialWeapon");
            Destroy(specialWeapon.gameObject);
            specialWeapon = null;
        }
    }


    public void TakeDamage(int damage)
    {
        //if shield is not active take damage
        if (!isShieldActive)
        {
            health -= damage;

            //Destory SpaceShip
            if (health <= 0)
            {
                OnSpaceShipDestoryed();
            }
            else {
                //Notify GameManager about taken damage
                GameManager.Instance.OnSpaceShipDamaged(health);
            }

        }
        else//If Shield is active, Disable shield
        {
            ToggleShield(false);
        }
    }



    //----Callbacks------

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(collision.gameObject.GetComponent<Asteroid>().damagePoints);
        }
    }

    void OnSpaceShipDestoryed() {
        health = 0;

        //Reset Rigidbody Physics
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0f;

        //hide Spaceship
        gameObject.SetActive(false);


        //Play Explosion Effect 
        VFXManager.Instance.ShowExplosionFX(transform.position);

        //Notify Game Manager this Ship has been destroyed
        GameManager.Instance.OnSpaceShipDestroyed(this);
    }


    //TODO Remove this
    public void OnPlayerCutSceneAnimationComplete()
    {
        cutSceneAnimationComplete = true;
        GetComponent<Animator>().enabled = false;
    }
    #endregion

}
