using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private CharacterStats myStats;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    void Start() 
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update() 
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats _targetStats) 
    {
        if(attackCooldown <= 0f) 
        {
            _targetStats.TakeDamage(myStats.damage.GetValue());
            attackCooldown = 1f / attackSpeed;
        }
    }
}

