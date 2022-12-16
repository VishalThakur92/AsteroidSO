using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class BlasterPowerUp : PowerUp,ICollectible
{
    #region Parameters
    [SerializeField]
    Weapon weapon;
    #endregion


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


    public void OnCollected() {
        //Reward player with this Powerup - Enable Blaster Mode for X seconds
        GameManager.Instance.playerSpaceShip.SetSpecialWeapon(weapon , duration);

        //StartCoroutine(BlasterModeBehaviour());
        //Destroy This powerup as has been collected
        Destroy(gameObject);
    }



    //IEnumerator BlasterModeBehaviour()
    //{
    //    yield return new WaitForSeconds(duration);
    //    GameManager.Instance.playerSpaceShip.DiscardSpecialWeapon();
    //    StopCoroutine(BlasterModeBehaviour());
    //}
    #endregion
}
