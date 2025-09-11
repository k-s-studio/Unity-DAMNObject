using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Assets.KsCode.DAMNObject.Editor {
    [CanEditMultipleObjects]
    [CustomEditor(typeof(DAMNObject))]
    public class SpineEditor : UnityEditor.Editor {
        private DAMNObject m_Target;
        public override VisualElement CreateInspectorGUI() {
            m_Target = (DAMNObject)target;
            return base.CreateInspectorGUI();
        }
        public override void OnInspectorGUI() {
            GUI.enabled = false;
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("serializeHelperList"));
            foreach (var e in m_Target.Entries)
                EditorGUILayout.ObjectField(e.Key?.ToString() ?? "null", e.Value, typeof(Component), true);
            GUI.enabled = true;
        }
    }
}