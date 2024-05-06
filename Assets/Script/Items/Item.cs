using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup", menuName = "Scriptables/Powerup")]
public class Item : ScriptableObject
{
    [Header("GameObjects & Components")]
    public Sprite icon = null; // Item icon

    [Header("Types")]
    public string powerupType = "Type";

    [Header("Specs")]
    public string Name = "Name"; // Name of the item
    public int damageModifier;
    public int armorModifier;
    public int speedModifier;
    public float dropRate;
    public bool isDefaultItem = false;
 
    public virtual void Use() 
    {
        Debug.Log("Using" + name);
    }
}
