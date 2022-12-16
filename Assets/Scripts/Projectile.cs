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

    //Duration for which this projectile will be there in the scene, after the duration it gets destroyed
    //public float maxLifetime;
    #endregion


    #region Methods
    //Project in the given Direction
    public virtual void Project(Vector2 direction)
    {}
    #endregion
}
