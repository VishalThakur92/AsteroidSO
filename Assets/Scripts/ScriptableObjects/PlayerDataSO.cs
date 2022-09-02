using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    //normal bullet prefab
    public Bullet bulletPrefab;


    //blaster bullet prefab
    public Bullet blasterBulletPrefab;

    //protective shield prefab
    public GameObject shieldPrefab;


    //maximym player health
    public int maxPlayerHealth = 100;


    //acceleration with which player moves
    public float acceleration = 1f;


    //player's rotation multiplier
    public float rotationSpeed = 0.1f;
}
