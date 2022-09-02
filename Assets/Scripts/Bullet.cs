using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : Projectile
{
    #region Methods
    public override void Project(Vector2 direction)
    {
        // The bullet only needs a force to be added once since they have no
        // drag to make them stop moving
        rigidbody.AddForce(direction * speed);

        // Destroy the bullet after it reaches it max lifetime
        Destroy(gameObject, maxLifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the bullet as soon as it collides with anything
        Destroy(gameObject);
    }
    #endregion
}
