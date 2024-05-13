using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityInput : MonoBehaviour
{
    protected CharacterController characterContr;
    protected GameObject player;
    protected PlayerStats stats;

    private static Dictionary<string, float> abilityCooldowns = new();

    [SerializeField] protected string abilityName = "Ability"; 
    protected abstract string AbilityName { get; } // Abstract property to get the ability name

    protected event Action<string> CooldownFinished;

    void Awake() 
    {
        characterContr = GetComponentInParent<CharacterController>();
        stats = GetComponentInParent<PlayerStats>();
        player = characterContr.gameObject;
    }

    protected Vector3 ReturnMousePosition(Transform _transform) 
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y; // Distance from the camera to the game world

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.y = _transform.position.y;

        return worldPosition;
    }
    protected IEnumerator AbilityCooldown(float cooldown)
    {
        if (!abilityCooldowns.ContainsKey(AbilityName))
        {
            abilityCooldowns.Add(AbilityName, cooldown); // Adding ability name and cooldown timer to the dictionary
            StartCoroutine(UpdateCooldown()); 
        }
        
        yield return null; 
    }

    private IEnumerator UpdateCooldown()
    {
        float cooldown = abilityCooldowns[AbilityName]; 

        yield return new WaitForSeconds(cooldown); 

        abilityCooldowns.Remove(AbilityName); 
        CooldownFinished?.Invoke(abilityName);
    }
}
