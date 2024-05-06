using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool hit;
    private readonly float timer = 4f;

    void Update() 
    {
        if(!hit) 
        {
            StartCoroutine(DestroyOverTime());
        }
    }

    void OnTriggerEnter(Collider collider) 
    {
        hit = true;
        Destroy(gameObject);

        if(collider.CompareTag(Tags.ENEMY)) 
        {
            if(collider.TryGetComponent<CharacterCombat>(out var combat) 
            && collider.TryGetComponent<CharacterStats>(out var stats)) 
            {
                combat.Attack(stats);
            }
        }
    }

    private IEnumerator DestroyOverTime() 
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
        yield break;
    }
}
