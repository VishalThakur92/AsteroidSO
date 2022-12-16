using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PowerUp : MonoBehaviour
{
    #region Params
    [SerializeField]
    //ScriptableObject Data
    public PowerUpDataSO powerUpData;

    public Rigidbody2D rigidbody;

    #endregion

    #region Methods

    private void Start()
    {
        //Destory when it has to die :(
        Destroy(gameObject, powerUpData.maxLifetime);
    }

    //Set Trajectory in which it shall be projected
    public void SetTrajectory(Vector2 direction)
    {
        // The asteroid only needs a force to be added once since they have no
        // drag to make them stop moving
        rigidbody.AddForce(direction * powerUpData.movementSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPowerUpCollectedBehaviour();
        }
    }



    void OnPowerUpCollectedBehaviour() {

        //Reward player with this Powerup
        GameManager.Instance.OnPowerUpCollectedBehaviour(this);

        // Destroy the current asteroid since it is either replaced by two
        // new asteroids or small enough to be destroyed by the bullet
        Destroy(gameObject);
    }

    #endregion
}
