using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PowerUp))]
public abstract class PowerUp : MonoBehaviour
{
    #region Params
    //Graphic of this powerup
    [SerializeField]
    protected SpriteRenderer image;

    //movement and Lifetime duration of this powerup
    //Duration - If 0 is infinite,  if greater than 0 then this powerup stays for specified seconds
    protected float movementSpeed, lifetime , duration;
    #endregion


    #region Methods
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
    }

    //set the provided sprite of this powerup
    public void SetSprite(Sprite sprite) {
        image.sprite = sprite;
    }

    public void Initialize(string name, Sprite sprite, float _movementSpeed , float _lifeTime , float _duration)
    {
        gameObject.name = name;
        image.sprite = sprite;
        movementSpeed = _movementSpeed;
        lifetime = _lifeTime;
        duration = _duration;
    }

    //Set Trajectory in which it shall be projected
    public virtual void SetTrajectory(Vector2 direction)
    {
        // The asteroid only needs a force to be added once since they have no
        // drag to make them stop moving
        this.GetComponent<Rigidbody2D>()?.AddForce(direction * movementSpeed);
    }


    public virtual void DefineLifeTime()
    {
        //Destory when it has to die :(
        Destroy(gameObject, lifetime);
    }
    #endregion
}
