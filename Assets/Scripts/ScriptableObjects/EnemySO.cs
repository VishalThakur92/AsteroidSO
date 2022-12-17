using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;

    //maximym player health
    public Sprite enemySprite;


    public float size = 1f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50f;
    public float maxLifetime = 30f;
    public int damagePoints = 50;
}
