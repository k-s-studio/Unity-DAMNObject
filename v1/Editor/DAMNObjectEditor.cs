using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Assets.KsCode.DAMNObject.v1.Editor {
    using ConfigDelegate = DamnConfig;

    [CustomPropertyDrawer(typeof(DamnModel))]
    public class DamnModelDrawer : PropertyDrawer {
        private static readonly string[] s_SelectBeginning = new[] { "none" };
        private static readonly Dictionary<Type, (string, ConfigDelegate)[]> m_ConfigsInType = new();
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            if (!m_ConfigsInType.ContainsKey(fieldInfo.DeclaringType)) {
                (string, ConfigDelegate)[] data = fieldInfo.DeclaringType.GetFields().Where(f => f.FieldType == typeof(ConfigDelegate)).Select(f => (f.Name, (ConfigDelegate)f.GetValue(property.serializedObject.targetObject))).ToArray();
                m_ConfigsInType.Add(fieldInfo.DeclaringType, data);
            }

            DamnModel target = (DamnModel)fieldInfo.GetValue(property.serializedObject.targetObject);

            ObjectField container = new(property.displayName);
            container.AddToClassList("unity-base-field__aligned");
            container.ElementAt(0).AddToClassList("unity-property-field__label");
            container.RemoveAt(1);

            Action createAct;
            (string name, ConfigDelegate action)[] lst = m_ConfigsInType[fieldInfo.DeclaringType];

            if (lst.Length == 0) createAct = () => target.Instantiate();
            else {
                DropdownField selection = new(s_SelectBeginning.Concat(lst.Select(e => e.name)).ToList(), 1);
                selection.BorderWidth(0, 0, 0, 0);
                selection.Margin(0, 0, 0, 1.6f);
                selection.Padding(0, 0, 0, 0);
                selection.style.minWidth = 66;
                selection.style.flexBasis = 0;
                selection.style.flexGrow = 1;
                selection.style.flexDirection = FlexDirection.Row;
                selection.style.overflow = Overflow.Visible;
                selection.style.textOverflow = TextOverflow.Clip;
                selection.style.whiteSpace = WhiteSpace.NoWrap;
                container.Add(selection);

                createAct = () => {
                    int index = selection.index - 1;
                    if (index < 0) target.Instantiate();
                    else target.Instantiate(lst[index].action);
                };
            }

            Button button = new(createAct) { text = "Create" };
            button.BorderWidth(1, 1, 1, 1);
            button.Margin(0, 0, 0, 0);
            button.Padding(0, 0, 20, 20);
            button.style.flexBasis = 0;
            button.style.flexGrow = 1;
            button.style.flexDirection = FlexDirection.Row;
            button.style.overflow = Overflow.Visible;
            button.style.textOverflow = TextOverflow.Clip;
            button.style.whiteSpace = WhiteSpace.NoWrap;
            if (container.childCount > 1) button.style.maxWidth = 70;
            container.Add(button);

            return container;
        }
    }
    [CustomPropertyDrawer(typeof(DAMNPrefab))]
    public class DAMNObjectDrawer : PropertyDrawer {
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            DAMNPrefab target = (DAMNPrefab)fieldInfo.GetValue(property.serializedObject.targetObject);
            ObjectField container = new(property.displayName);
            container.AddToClassList("unity-base-field__aligned");
            container.ElementAt(0).AddToClassList("unity-property-field__label");
            container.RemoveAt(1);

            Button button = new(() => target.Instantiate()) { text = "Create" };
            button.BorderWidth(1, 1, 1, 1);
            button.Margin(0, 0, 0, 0);
            button.Padding(0, 0, 20, 20);
            button.style.flexBasis = 0;
            button.style.flexGrow = 1;
            button.style.flexDirection = FlexDirection.Row;
            button.style.overflow = Overflow.Visible;
            button.style.textOverflow = TextOverflow.Clip;
            button.style.whiteSpace = WhiteSpace.NoWrap;
            if (container.childCount > 1) button.style.maxWidth = 70;
            container.Add(button);

            return container;
        }
    }
}
