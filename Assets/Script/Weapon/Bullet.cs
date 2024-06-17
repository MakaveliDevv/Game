using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool hit;
    private readonly float timer = 4f;
    [SerializeField] Weapon _Weapon;

    void Update() 
    {
        if(!hit) 
        {
            StartCoroutine(DestroyOverTime());
        }
    }

    void FixedUpdate() 
    {
        transform.Translate(Time.deltaTime * _Weapon.bulletVelocity * Vector3.forward);
    }

    void OnTriggerEnter(Collider collider) 
    {
        if(collider.CompareTag("Enemy")) 
        {
            Debug.Log("Enemy hit");
            if(collider.TryGetComponent<CharacterCombat>(out var combat) 
            && collider.TryGetComponent<EnemyStats>(out var enemyStats)) 
            {
                Debug.Log("Fetched both scripts");
                hit = true;
                combat.Attack(enemyStats);
                Destroy(gameObject);
            }
        }
    }

    // void OnTriggerStay(Collider other) 
    // {
    //     if(other.gameObject.layer == gameObject.layer && other.gameObject.name != "Player")
    //     {
    //         hit = true;
    //         Debug.Log(other.gameObject.name);
    //         Destroy(gameObject);
    //     } 
    // }

    private IEnumerator DestroyOverTime() 
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
        yield break;
    }
}
