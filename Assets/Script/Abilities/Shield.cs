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
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
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
        if(vfxObj != null && !shieldCooldown)  
        {
            StartCoroutine(AbilityCooldown(shieldCoolDownTimer));
            shieldCooldown = true;

            vfxObj.SetActive(true);
        }

        yield return new WaitForSeconds(shieldTimer);

        vfxObj.SetActive(false);

        yield return null;

    }
}

