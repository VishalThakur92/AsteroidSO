using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    #region Params
    [SerializeField]
    //ScriptableObject Data
    protected PowerUpDataSO powerUpData;

    [SerializeField]
    protected Rigidbody2D rigidbody;

    //If 0 is infinite,  if greater than 0 then this powerup stays for specified seconds
    [SerializeField]
    protected float duration;
    #endregion


    #region Methods


    //Set Trajectory in which it shall be projected
    public virtual void SetTrajectory(Vector2 direction)
    {
        // The asteroid only needs a force to be added once since they have no
        // drag to make them stop moving
        rigidbody.AddForce(direction * powerUpData.movementSpeed);
    }


    public virtual void DefineLifeTime()
    {
        //Destory when it has to die :(
        Destroy(gameObject, powerUpData.maxLifetime);
    }
    #endregion
}
