using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat currentHealth;
    public Stat damage;
    public Stat armor;
    // Shield power? Like larger radius
    public Stat walkSpeed;
    public Stat runSpeed;
    
    // UI
    [Header("Stats")]
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI moveSpeedText;

    // [Header("Modifiers")]
    // public TextMeshProUGUI maxHp_Mod;
    // public TextMeshProUGUI Armor_Mod;
    // public TextMeshProUGUI Dmg_Mod;
    // public TextMeshProUGUI MoveSp_Mod;

    protected GameManager gameManager_instance;

    void Awake() 
    {
        gameManager_instance = GameManager.instance;
        currentHealth.SetValue(maxHealth.ReturnBaseValue());

        // UI
        if(transform.gameObject.CompareTag("Player"))
        {
            maxHealthText.text = maxHealth.ReturnBaseValue().ToString();
            currentHealthText.text = currentHealth.ReturnBaseValue().ToString();
            damageText.text = damage.ReturnBaseValue().ToString();
            armorText.text = armor.ReturnBaseValue().ToString();
            moveSpeedText.text = walkSpeed.ReturnBaseValue().ToString();
        }
    }

    public void TakeDamage(float incomingDamage)
    {
        // For armor
        float damageReductionPercentage = armor.GetPercentageReduction(50f);
        float damageReduction = incomingDamage * (damageReductionPercentage / 100f);
        
        int finalDamage = Mathf.RoundToInt(incomingDamage - damageReduction);
        finalDamage = Mathf.Clamp(finalDamage, 0, int.MaxValue);

        // Apply the final damage to the character's health
        currentHealth.SetValue(currentHealth.GetValue() - finalDamage);

        if(transform.gameObject.CompareTag("Player"))
            currentHealthText.text = currentHealth.ToString(); 
        // Debug.Log(transform.name + " takes " + finalDamage + " damage.");

        if (currentHealth.GetValue() <= 0f)
        {
            currentHealth.SetValue(0);
            Die();
        }
    }

    public virtual void Die() 
    {
        // This method is ment to be overwritten
        // Die in some way
        // Debug.Log(transform.name + "died.");
    }
}
