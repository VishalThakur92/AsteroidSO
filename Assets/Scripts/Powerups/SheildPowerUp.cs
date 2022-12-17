using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class SheildPowerUp : PowerUp,ICollectible
{
    #region Methods

    void Start() {
        base.DefineLifeTime();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCollected();
        }
    }


    //Define what happens when this powerup is collected
    public void OnCollected() {
        //Reward player with this Powerup - Enable Spaceship Shield
        GameManager.Instance.playerSpaceShip.ToggleShield(true);
        
        //Destroy This powerup as has been collected
        Destroy(gameObject);
    }
    #endregion
}
