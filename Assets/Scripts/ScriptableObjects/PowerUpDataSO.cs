using UnityEngine;

[CreateAssetMenu(menuName="Data/PowerUpData")]
public class PowerUpDataSO : ScriptableObject
{

    //this power up's Type
    public Globals.Powerups powerUpType;

    //movement speed of this Powerup
    public float movementSpeed = 50f;

    //duration for which this powerup is active
    public float maxLifetime = 30f;


}