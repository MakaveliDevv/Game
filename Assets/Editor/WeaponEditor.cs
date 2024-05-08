using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Weapon weapon = (Weapon)target;

        // GameObjects & Components
        EditorGUILayout.PropertyField(serializedObject.FindProperty("wpnObject"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("muzzleFlash"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bullet"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("icon"));

        // Types
        EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireType"));

        // Specs
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Name"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletDamage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletVelocity"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireRate"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("range"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nextTimeToFire"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waitBeforeEquipTime"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("equipTimer"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponEquipped"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultWeapon"));
        

        // Convert percentage drop rate to value between 0 and 1
        SerializedProperty dropRateProperty = serializedObject.FindProperty("dropRate");
        float dropRatePercentage = EditorGUILayout.FloatField("Drop Rate (%)", weapon.dropRate * 100f);
        dropRateProperty.floatValue = Mathf.Clamp(dropRatePercentage / 100f, 0f, 1f);

        serializedObject.ApplyModifiedProperties();
    }
}
