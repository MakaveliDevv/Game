using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField] private float baseValue;
    [SerializeField] private List<float> modifiers = new();
    private float initialValue;

    public float GetValue() 
    {
        float finalValue = baseValue;
        foreach(float modifier in modifiers) 
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    public void SetValue(float value) 
    {
        baseValue = value;
    }

    public float ReturnBaseValue() 
    {
        return baseValue;
    }

    public float GetPercentageReduction(float percentage)
    {
        return Mathf.Clamp(GetValue(), 0, percentage);
    }
    
    public void AddModifier(float modifier) 
    {
        if(modifier != 0) 
            modifiers.Add(modifier);
    }

    public void RemoveModifier(float modifier) 
    {
        if(modifier != 0) 
            modifiers.Remove(modifier);
    }
}
