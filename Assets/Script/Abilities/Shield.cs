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
}

