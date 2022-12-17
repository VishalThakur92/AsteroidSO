using UnityEngine;

[CreateAssetMenu(menuName="Data/PowerUp_SO")]
public class PowerUpSO : ScriptableObject
{

    [Tooltip("Name of this powerup")]
    public string powerupName;

    [Tooltip("Graphic of this Powerup")]
    public Sprite sprite;

    [Tooltip("Custom Behaviour of this powerup as scripted by the developer")]
    public PowerUp customBehaviourPrefab;

    [Tooltip("Movement speed of this Powerup")]
    public float movementSpeed = 50f;

    [Tooltip("Powerup will be destroyed after this many seconds")]
    public float lifetime = 30f;

    [Tooltip("Duration for which this powerup will be in effect once collected")]
    public float duration = 30f;
}