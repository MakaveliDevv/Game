using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class PowerupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Item item = (Item)target;

        // GameObjects & Components
        EditorGUILayout.PropertyField(serializedObject.FindProperty("icon"));

        // Types
        EditorGUILayout.PropertyField(serializedObject.FindProperty("powerupType"));

        // Specs
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Name"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("damageModifier"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("armorModifier"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speedModifier"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealthModifier"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cooldownTimer"));

        // Convert percentage drop rate to value between 0 and 1
        SerializedProperty dropRateProperty = serializedObject.FindProperty("dropRate");
        float dropRatePercentage = EditorGUILayout.FloatField("Drop Rate (%)", item.dropRate * 100f);
        dropRateProperty.floatValue = Mathf.Clamp(dropRatePercentage / 100f, 0f, 1f);

        serializedObject.ApplyModifiedProperties();
    }
}
