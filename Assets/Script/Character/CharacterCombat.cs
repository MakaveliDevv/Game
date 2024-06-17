using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public CharacterStats myStats;

    void Start() 
    {
        myStats = GetComponent<CharacterStats>();
    }


    public void Attack(CharacterStats _targetStats) 
    {
        _targetStats.TakeDamage(myStats.damage.GetValue());
        Debug.Log(_targetStats.currentHealth.GetValue());
    }
}

