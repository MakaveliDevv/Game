using System.Collections;
using UnityEngine;

public class Teleport : AbilityInput
{
    [SerializeField] private float teleportCooldownTimer = 2f;
    [SerializeField] private float teleportDistance = 10f;
    private readonly float teleportDuration = 0.05f;
    [SerializeField] private bool teleportCooldown;

    protected override string AbilityName => abilityName; // Override to specify the ability name

    void Start()
    {
        CooldownFinished += OnCooldownFinished; // Subscribe to the cooldown finished event
    }

    void Update() 
    {
        if (Input.GetButtonDown("Fire2"))
        {
            // Teleport
            TeleportInput();
        }
    }

    private void OnCooldownFinished(string abilityName)
    {
        if (abilityName == AbilityName)
        {
            teleportCooldown = false; // Reset dashCooldown flag to false
        }
    }

    private void TeleportInput() 
    {
        Vector3 direction = (ReturnMousePosition(player.transform) - player.transform.position).normalized;
        Vector3 targetPosition = player.transform.position + direction * teleportDistance;
        targetPosition.y = player.transform.position.y;

        if(!teleportCooldown) 
        {
            StartCoroutine(AbilityCooldown(teleportCooldownTimer));
            
            StartCoroutine(TeleportTowardsMousePosition(targetPosition));

            PlayerManager.instance.previousState = PlayerManager.instance.state; // Store the previous state
            PlayerManager.instance.state = PlayerManager.PlayerState.TELEPORT; // Switch to Dash state

            teleportCooldown = true;
        }
    }

    private IEnumerator TeleportTowardsMousePosition(Vector3 _targetPosition)
    {
        vfxObj.SetActive(true);           

        Vector3 startPosition = player.transform.position;
        float distance = Vector3.Distance(startPosition, _targetPosition);
        float duration = Mathf.Min(distance, teleportDuration);
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            // Lerp towards the teleport destination
            float t = (Time.time - startTime) / duration;
            Vector3 newPosition = Vector3.Lerp(player.transform.position, _targetPosition, t);
            characterContr.Move(newPosition - player.transform.position);
     

            yield return null; 
        }

        player.transform.position = _targetPosition;
        PlayerManager.instance.state = PlayerManager.instance.previousState; // Switch to the previous state
    }
}

