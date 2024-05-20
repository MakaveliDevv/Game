using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbilityInput : MonoBehaviour
{
    protected CharacterController characterContr;
    protected GameObject player;
    [SerializeField] protected GameObject vfxObj;
    [SerializeField] protected GameObject image;
    [SerializeField] protected Image img;
    [SerializeField] protected Color color;

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
        color = img.color;
        float currentA = color.a;
        float aDecrease = currentA * .25f;
        float a = currentA - aDecrease;
        color.a -= a;
        img.color = color;
        
        yield return new WaitForSeconds(cooldown); 

        vfxObj.SetActive(false);
        color.a += a;
        img.color = color;

        abilityCooldowns.Remove(AbilityName); 
        CooldownFinished?.Invoke(abilityName);
    }
}
