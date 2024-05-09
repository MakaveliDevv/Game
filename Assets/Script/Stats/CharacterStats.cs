using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat currentHealth;
    public Stat damage;
    public Stat armor;
    public Stat walkSpeed;
    public Stat runSpeed;
    public Stat dash;
    
    protected GameManager gameManager_instance;

    void Awake() 
    {
        gameManager_instance = GameManager.instance;
        currentHealth = maxHealth;
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
