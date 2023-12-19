using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Stat damage;
    public Stat armor;

    void Awake() 
    {
        currentHealth = maxHealth;
    }

    void Update() 
    {
        // if(Input.GetKeyDown(KeyCode.T)) 
        // {
        //     TakeDamage(10);
        // }
    }

    public void TakeDamage(float incomingDamage)
    {
        float damageReductionPercentage = armor.GetPercentageReduction(50f);
        float damageReduction = incomingDamage * (damageReductionPercentage / 100f);
        
        int finalDamage = Mathf.RoundToInt(incomingDamage - damageReduction);
        finalDamage = Mathf.Clamp(finalDamage, 0, int.MaxValue);

        // Apply the final damage to the character's health
        currentHealth -= finalDamage;
        Debug.Log(transform.name + " takes " + finalDamage + " damage.");

        if (currentHealth <= 0f)
        {
            currentHealth = 0;
            Die();
        }
    }

    public virtual void Die() 
    {
        // This method is ment to be overwritten
        // Die in some way
        Debug.Log(transform.name + "died.");
    }
}
