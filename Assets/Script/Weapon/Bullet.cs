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
        if(collider.CompareTag(Tags.ENEMY)) 
        {
            if(collider.TryGetComponent<CharacterCombat>(out var combat) 
            && collider.TryGetComponent<EnemyStats>(out var stats)) 
            {
                combat.Attack(stats);
            }
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.layer == gameObject.layer && other.gameObject.name != "Player")
        {
            hit = true;
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        } 
    }

    private IEnumerator DestroyOverTime() 
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
        yield break;
    }
}
