using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Dash : AbilityInput
{
    [SerializeField] private float maxDashDistance = 5f;
    [SerializeField] private float dashCooldownTimer; 
    [SerializeField] private float dashLerpDuration = 0.2f; 
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private bool dashCoolDown;
    // [SerializeField] private ParticleSystem vfx;

    protected override string AbilityName => abilityName; // Override to specify the ability name

    void Start()
    {
        CooldownFinished += OnCooldownFinished; // Subscribe to the cooldown finished event
        // vfx = vfxObj.GetComponent<ParticleSystem>();
        // vfx.time = 1.25f;
        // var main = vfx.main;
        // main.simulationSpeed = 2f;
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            DashInput();
        }
    }

    private void OnCooldownFinished(string abilityName)
    {
        if (abilityName == AbilityName)
            dashCoolDown = false; 
    }
    
    private void DashInput() 
    {                
        Vector3 direction = (ReturnMousePosition(player.transform) - player.transform.position).normalized;
        Vector3 targetPosition = player.transform.position + direction * Mathf.Min(maxDashDistance); 
        targetPosition.y = player.transform.position.y;

        if(!dashCoolDown) 
        {
            StartCoroutine(AbilityCooldown(dashCooldownTimer));
            dashCoolDown = true;

            StartCoroutine(DashTowardsPosition(targetPosition));

            PlayerManager.instance.previousState = PlayerManager.instance.state; // Store the previous state
            PlayerManager.instance.state = PlayerManager.PlayerState.DASH; // Switch to Dash state

        }
    }

    private IEnumerator DashTowardsPosition(Vector3 _targetPosition)
    {
        Vector3 startPosition = player.transform.position;
        float distance = Vector3.Distance(startPosition, _targetPosition);
        float duration = Mathf.Min(distance / dashSpeed, dashLerpDuration);
        float startTime = Time.time;

        vfxObj.SetActive(true);           

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            Vector3 newPosition = Vector3.Lerp(startPosition, _targetPosition, t);
            characterContr.Move(dashSpeed * Time.deltaTime * (newPosition - player.transform.position));

            yield return null;
        }

        player.transform.position = _targetPosition;
        PlayerManager.instance.state = PlayerManager.instance.previousState; // Switch to the previous state
    }

    // private IEnumerator DashTowardsPosition(Vector3 _targetPosition)
    // {
    //     Vector3 startPosition = player.transform.position;
    //     float distance = Vector3.Distance(startPosition, _targetPosition);
    //     float duration = Mathf.Min(distance / dashSpeed, dashLerpDuration);
    //     float startTime = Time.time;

    //     while (Time.time < startTime + duration)
    //     {
    //         float t = (Time.time - startTime) / duration;
    //         Vector3 newPosition = Vector3.Lerp(startPosition, _targetPosition, t);
    //         characterContr.Move(dashSpeed * Time.deltaTime * (newPosition - player.transform.position));

    //         yield return null;
    //     }

    //     player.transform.position = _targetPosition;

    //     PlayerManager.instance.state = PlayerManager.instance.previousState; // Switch to the previous state
    // }
}
