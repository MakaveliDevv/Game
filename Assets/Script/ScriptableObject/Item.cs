using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Powerup", menuName = "Scriptables/Powerup")]
public class Item : ScriptableObject
{
    [Header("GameObjects & Components")]
    public Sprite icon = null; // Item icon

    [Header("Types")]
    public string powerupType = "Type";

    [Header("Specs")]
    public string Name = "Name"; // Name of the item
    public int maxHealthModifier;
    public int damageModifier;
    public int armorModifier;
    public int movementModifier;
    public int cooldownTimer;
    public float dropRate;
    public bool isDefaultItem = false;
    public TextMeshProUGUI cooldownText;
}
