using UnityEngine;
using UnityEditor;
using CustomEnums;

[CustomEditor(typeof(KeyBehavior))]
public class KeyBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var keyBehavior = target as KeyBehavior;

        keyBehavior.keyType = (KeyType) EditorGUILayout.EnumPopup("Key Type: ", keyBehavior.keyType);

        if(keyBehavior.keyType == KeyType.Value)
        {
            keyBehavior.command = Commands.None;

            keyBehavior.shiftable = EditorGUILayout.Toggle("Shiftable Value: ", keyBehavior.shiftable);

            var unshiftLabel = (keyBehavior.shiftable) ? "Unshifted Value: " : "Value: ";
            keyBehavior.valueUnshifted = EditorGUILayout.TextField(unshiftLabel, keyBehavior.valueUnshifted);

            if (keyBehavior.shiftable)
                keyBehavior.valueShifted = EditorGUILayout.TextField("Shifted Value: ", keyBehavior.valueShifted);
        }
        else
        {
            keyBehavior.shiftable = false;
            keyBehavior.valueUnshifted = null;
            keyBehavior.valueShifted = null;

            keyBehavior.command = (Commands) EditorGUILayout.EnumPopup("Command: ", keyBehavior.command);
        }
    }
}
