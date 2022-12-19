using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class SpaceShip : MonoBehaviour, IDamageable
{
    #region Params
    [SerializeField]
    GameObject shieldPrefab;

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

    //public float respawnDelay = 3f;
    public float respawnInvulnerability = 3f;

    //current health of player
    public int health;

    //is shield active right now ?
    public bool isShieldActive = false;

    //inital Cut scene has been completed, if true continues to main gameplay
    [SerializeField]
    bool canControl = false;

    SpriteRenderer image;

    float aceeleration;

    float rotationSpeed;
    #endregion


    #region Methods
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        image = GetComponent<SpriteRenderer>();
    }


    public void Initalize(string _name, Sprite _sprite ,int _maxPlayerHealth ,float _acceleration , float _rotationSpeed) {
        gameObject.name = _name;
        image.sprite = _sprite;
        health = _maxPlayerHealth;
        aceeleration = _acceleration;
        rotationSpeed = _rotationSpeed;

        //Toggle Shield off by default
        ToggleShield(false);
    }


    void Update()
    {
        if (!canControl)
            return;
            
        HandlePlayerInput();
    }


    //Handle User Input 
    void HandlePlayerInput() {

        //Movement
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            turnDirection = -1f;
        }
        else{
            turnDirection = 0f;
        }

        //Shooting
        if (Input.GetKeyDown(KeyCode.Space)){
            defaultweapon.Shoot();
        }
    }

    //Movement 
    void FixedUpdate()
    {
        if (!canControl)
            return;

        if (thrusting) {
            rigidbody.AddForce(transform.up * aceeleration);
        }

        if (turnDirection != 0f) {
            rigidbody.AddTorque(rotationSpeed * turnDirection);
        }
    }


    //Toggle Shield On or Off
    public void ToggleShield(bool flag) {
        if (flag)
        {
            if (myShield != null)
                Destroy(myShield);

            myShield = Instantiate(shieldPrefab, this.transform, false);
        }
        else {
            if(myShield != null)
                Destroy(myShield);
        }

        isShieldActive = flag;
    }


    
    public void EquipSpecialWeapon(Weapon weapon , float duration)
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
                OnDestoryed();
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

    public void ToggleCanControl(bool flag) {
        canControl = flag;
        GetComponent<Animator>().enabled = flag;
    }


    //----Callbacks------
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(collision.gameObject.GetComponent<Asteroid>().damagePoints);
        }
    }

    void OnDestoryed() {
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
    #endregion

}
