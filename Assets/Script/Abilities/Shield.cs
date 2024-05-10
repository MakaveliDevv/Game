using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : AbilityInput
{
    public float knockbackForce = 10f;
    [SerializeField] private float shieldRadius;
    protected override string AbilityName => abilityName; 

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            ActivateShield();
        }
    }
    
    // Activate the shield
    public bool ActivateShield() 
    {
        // Add a sphere collider
        SphereCollider sphere = transform.AddComponent<SphereCollider>();
        sphere.radius = shieldRadius;


        return false;
    }

    // JUST CREATE A SPHERE COLLIDER ON THIS SCRIPT WHICH REPRESENTS THE SHIELD
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Detected!");


            // // Calculate knockback direction
            // Vector3 knockbackDirection = other.transform.position - transform.position;
            // knockbackDirection = knockbackDirection.normalized;

            // // Apply knockback force
            // other.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }
}

