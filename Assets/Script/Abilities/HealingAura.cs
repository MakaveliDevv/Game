using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAura : AbilityInput
{   
    protected override string AbilityName => abilityName;

    // Healing over time ability parameters
    public float healAmountPerTick = 5f; // Amount of health restored per tick
    public float healTickInterval = 1f; // Time interval between each healing tick
    public float healDuration = 5f; // Duration of the healing ability

    private bool isHealingActive = false;
    private bool healingCooldown;

    void Start() 
    {
        CooldownFinished += OnCoolDownFinished;
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.H)) 
        {
            Debug.Log("Healing ability activated!");
            ActivateHealingAbility();
        }
    }

    void OnCoolDownFinished(string abilityName) 
    {
        if(abilityName == AbilityName)
            healingCooldown = false; 
    }

    // Method to activate the healing over time ability
    public void ActivateHealingAbility()
    {
        if (!isHealingActive && !healingCooldown)
        {
            // Check if the current health is not equal to max health
            StartCoroutine(HealOverTimeCoroutine());
            healingCooldown = true;
        }
    }

    // Coroutine for the healing over time ability
    private IEnumerator HealOverTimeCoroutine()
    {
        isHealingActive = true;

        float elapsedTime = 0f;

        while (elapsedTime < healDuration)
        {
            // Heal the player
            stats.currentHealth.SetValue(Mathf.Min(stats.currentHealth.GetValue() + healAmountPerTick, stats.maxHealth.GetValue()));

            // Wait for the next healing tick
            yield return new WaitForSeconds(healTickInterval);

            // Update elapsed time
            elapsedTime += healTickInterval;
        }

        isHealingActive = false;
    }

}
