using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityInput : MonoBehaviour
{

    [SerializeField] private float maxDashDistance = 5f; 
    [SerializeField] private float dashCooldown = .5f; 
    [SerializeField] private float dashLerpDuration = 0.2f; 
    [SerializeField] private float dashSpeed = 10f;

    private bool cooldown;

    void Update() 
    {

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Dash();
        }
    }

    public void Dash() 
    {   
        if (!cooldown) 
        {
            PlayerManager.instance.previousState = PlayerManager.instance.state; // Store the previous state
            PlayerManager.instance.state = PlayerManager.PlayerState.DASH; // Switch to Dash state


            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.y; // Distance from the camera to the game world

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 direction = (worldPosition - transform.position).normalized;

            Vector3 targetPosition = transform.position + direction * Mathf.Min(maxDashDistance); 
            targetPosition.y = transform.position.y;

            StartCoroutine(DashToPosition(targetPosition));
            StartCoroutine(AbilityCooldown());

        }
    }

    IEnumerator DashToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = Mathf.Min(distance / dashSpeed, dashLerpDuration); // Choose the shortest duration
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t); // Smoothly lerp between start and target positions
            yield return null;
        }

        transform.position = targetPosition;

        PlayerManager.instance.state = PlayerManager.instance.previousState;
    }

    IEnumerator AbilityCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        cooldown = false;
    }

}
