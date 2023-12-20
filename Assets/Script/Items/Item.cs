using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup", menuName = "Scriptables/Powerup")]
public class Item : ScriptableObject
{
    new public string name = "Name"; // Name of the item
    public string powerupType = "Type";
    public int damageModifier;
    public int armorModifier;
    public int speedModifier;
    public Sprite icon = null; // Item icon
    public bool isDefaultItem = false;
 
    public virtual void Use() 
    {
        Debug.Log("Using" + name);
    }
}
