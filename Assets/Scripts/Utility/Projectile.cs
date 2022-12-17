using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    #region Params
    //Projectile's Rigidbody component
    public new Rigidbody2D rigidbody;

    //Projectile's projecting speed
    public float speed;
    #endregion


    #region Methods
    //Project in the given Direction
    public virtual void Project(Vector2 direction)
    {}
    #endregion
}
