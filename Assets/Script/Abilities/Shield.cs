using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : AbilityInput
{
    public LayerMask enemyLayerMask;
    [SerializeField] private float shieldRadius;
    [SerializeField] private float shieldCoolDownTimer;
    [SerializeField] private float shieldTimer;
    [SerializeField] private bool shieldCooldown;

    protected override string AbilityName => abilityName; 

    void Start()
    {
        CooldownFinished += OnCooldownFinished; // Subscribe to the cooldown finished event
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            StartCoroutine(ShieldInput());
        }
    }

    private void OnCooldownFinished(string abilityName) 
    {
        if(abilityName == AbilityName) 
            shieldCooldown = false;
    } 

    private IEnumerator ShieldInput()
    {
        SphereCollider coll = GetComponent<SphereCollider>();
        if(coll == null && !shieldCooldown) 
        {
            SphereCollider sphere = transform.AddComponent<SphereCollider>();
            sphere.radius = shieldRadius;
            sphere.center = new Vector3(0, .85f, 0);
        }

        yield return new WaitForSeconds(shieldTimer);

        SphereCollider sphereCollider = transform.GetComponent<SphereCollider>();
        Destroy(sphereCollider);

        yield return null;

        StartCoroutine(AbilityCooldown(shieldCoolDownTimer));
        shieldCooldown = true;
    }
    
    // Activate the shield
    // public void ActivateShield() 
    // {
    //     SphereCollider sphere = transform.AddComponent<SphereCollider>();
    //     sphere.radius = shieldRadius;
    //     sphere.center = new Vector3(0, .85f, 0);

    //     Collider[] colliders = Physics.OverlapSphere(transform.position, shieldRadius, enemyLayerMask);

    //     foreach (var collider in colliders)
    //     {
    //         if(collider.TryGetComponent<IKnockbackable>(out IKnockbackable knockbackable)) 
    //         {
    //             Debug.Log("Fetched the IKnockbackable component");
                
    //             Vector3 force = (collider.transform.position - transform.position).normalized * knockbackForce;
    //             knockbackable.GetKnockedBack(force);

    //             Debug.Log("Knocked back from overlap sphere");
    //         }
    //     }
    // }

    // private void OnCollisionEnter(Collision other) 
    // {
    //     if(other.gameObject.CompareTag("Enemy")) 
    //     {
    //         if(other.gameObject.TryGetComponent<IKnockbackable>(out IKnockbackable knockbackable)) 
    //         {
    //             Vector3 force = other.contacts[0].normal;
    //             knockbackable.GetKnockedBack(force);
    //             Debug.Log("Knocked back from collision detection");
    //         }
    //     }
    // }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shieldRadius);
    }

    // JUST CREATE A SPHERE COLLIDER ON THIS SCRIPT WHICH REPRESENTS THE SHIELD
    // private void OnTriggerEnter(Collider other)
    // {
    //     // Check if the collider belongs to an enemy
    //     if (other.CompareTag("Enemy"))
    //     {
    //         Debug.Log("Enemy Detected!");


    //         // // Calculate knockback direction
    //         // Vector3 knockbackDirection = other.transform.position - transform.position;
    //         // knockbackDirection = knockbackDirection.normalized;

    //         // // Apply knockback force
    //         // other.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    //     }
    // }

}

