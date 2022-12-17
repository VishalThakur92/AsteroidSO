using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour, IDamageable
{
    #region Params
    public new Rigidbody2D rigidbody { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }

    //public Sprite[] sprites;



    public float size = 1f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50f;
    public float maxLifetime = 30f;
    public int damagePoints = 50;
    #endregion


    #region Methods
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialze(string name, Sprite sprite , float _size , float _minSize , float _maxSize , float _movementSpeed , float _maxLifeTime, int _damagePoints)
    {
        gameObject.name = name;
        minSize = _minSize;
        maxSize = _maxSize;
        movementSpeed = _movementSpeed;
        maxLifetime = _maxLifeTime;
        damagePoints = _damagePoints;
        // Assign random properties to make each asteroid feel unique
        spriteRenderer.sprite = sprite;
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        // Set the scale and mass of the asteroid based on the assigned size so
        // the physics is more realistic
        transform.localScale = Vector3.one * size;
        rigidbody.mass = size;

        // Destroy the asteroid after it reaches its max lifetime
        Destroy(gameObject, maxLifetime);
    }


    //Set Asteroid's Trajectory in which it will project
    public void SetTrajectory(Vector2 direction)
    {
        // The asteroid only needs a force to be added once since they have no
        // drag to make them stop moving
        rigidbody.AddForce(direction * movementSpeed);
    }

    //Asteroid Spliting algo
    private Asteroid CreateSplit()
    {
        // Set the new asteroid poistion to be the same as the current asteroid
        // but with a slight offset so they do not spawn inside each other
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        // Create the new asteroid at half the size of the current
        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;

        // Set a random trajectory
        half.SetTrajectory(Random.insideUnitCircle.normalized);

        return half;
    }



    //-----Callbacks--------

    //Handle how this asteroid behaves upon being hit by bullet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(0);
        }
    }


    //Cut scene Algo
    public void CutSceneBehaviour() {
        StartCoroutine(LerpIntoScene());
    }

    //Cut scene Algo
    IEnumerator LerpIntoScene() {

        while (gameObject.activeSelf) {
            transform.position = Vector3.Lerp(transform.position ,new Vector3( 7.45f , 0 ,0 ) , Time.deltaTime * .5f);
            yield return null;
        }
    }

    public void TakeDamage(int damage)
    {
        // Check if the asteroid is large enough to split in half
        // (both parts must be greater than the minimum size)
        if ((size * 0.5f) >= minSize)
        {
            CreateSplit();
            CreateSplit();
        }

        //Play hit effect FX
        VFXManager.Instance.ShowExplosionFX(transform.position);

        //Notify Game Manager about this asteroid being destoroyed
        GameManager.Instance.AsteroidDestroyed(this);

        // Destroy the current asteroid since it is either replaced by two
        // new asteroids or small enough to be destroyed by the bullet
        Destroy(gameObject);
    }
    #endregion



}
