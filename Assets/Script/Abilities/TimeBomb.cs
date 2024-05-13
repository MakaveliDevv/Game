using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : AbilityInput
{
    public float explosionRadius = 5f; // Radius of the explosion
    public float explosionDamage = 50f; // Damage inflicted by the explosion
    public float detonationDelay = .5f; // Delay before the bomb detonates

    [SerializeField] private bool bombCooldown;
    [SerializeField] private float bombCooldownTimer;

    protected override string AbilityName => abilityName; 
    // Start is called before the first frame update
    void Start()
    {
        CooldownFinished += OnCooldownFinished; // Subscribe to the cooldown finished event
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.B)) 
        {
            DeployTimeBomb();
        }
    }

    private void OnCooldownFinished(string abilityName)
    {
        if (abilityName == AbilityName)
            bombCooldown = false; 
    }

    // Method to deploy the time bomb
    public void DeployTimeBomb()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    // Coroutine to detonate the bomb after a delay
    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(detonationDelay);

        Explode();

        StartCoroutine(AbilityCooldown(bombCooldownTimer));
        bombCooldown = true;
    }

    // Method to trigger the explosion
    private void Explode()
    {
        // Instantiate explosion effects
        // Instantiate(explosionPrefab, transform.position, Quaternion.identity); 

        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to an enemy
            if (collider.CompareTag("Enemy"))
            {
                // Damage the enemy
                CharacterStats enemyStats = collider.GetComponent<CharacterStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(explosionDamage);
                }
            }
        }

        // Destroy the bomb GameObject
        // Destroy(gameObject);
    }
}
