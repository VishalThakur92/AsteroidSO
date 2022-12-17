using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player")]
public class PlayerDataSO : ScriptableObject
{
    public string spaceShipName;

    //maximym player health
    public Sprite spaceShipSprite;

    //maximym player health
    public int maxPlayerHealth = 100;

    [Range(.1f,2)]
    //acceleration with which player moves
    public float acceleration = 1f;

    [Range(.05f, 1f)]
    //player's rotation multiplier
    public float rotationSpeed = 0.1f;
}
