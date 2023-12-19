using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item"; // Name of the item
    public string powerupType = "type";
    public int damageModifier;
    public int armorModifier;
    public int speedModifier;
    public Sprite icon = null; // Item icon
    public bool isDefaultItem = false; // Is the item default wear?
 
    public virtual void Use() 
    {
        Debug.Log("Using" + name);
    }
}
