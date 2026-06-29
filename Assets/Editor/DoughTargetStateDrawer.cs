using UnityEditor;
using UnityEngine;

namespace GWBGameJam
{
    [CustomPropertyDrawer(typeof(DoughTargetStateAttribute))]
    public class DoughTargetStateDrawer : PropertyDrawer
    {
        private static readonly DoughState[] ValidStates =
        {
            DoughState.Softest,
            DoughState.Medium,
            DoughState.Hardest
        };

        private static readonly GUIContent[] Labels =
        {
            new GUIContent(nameof(DoughState.Softest)),
            new GUIContent(nameof(DoughState.Medium)),
            new GUIContent(nameof(DoughState.Hardest))
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Enum)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            DoughState current = (DoughState)property.enumValueIndex;
            int selectedIndex = System.Array.IndexOf(ValidStates, current);
            if (selectedIndex < 0)
                selectedIndex = 1;

            EditorGUI.BeginProperty(position, label, property);
            selectedIndex = EditorGUI.Popup(position, label, selectedIndex, Labels);
            property.enumValueIndex = (int)ValidStates[selectedIndex];
            EditorGUI.EndProperty();
        }
    }
}
