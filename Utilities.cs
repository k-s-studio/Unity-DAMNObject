using UnityEngine.UIElements;

namespace Assets.KsCode.DAMNObject {
    internal static partial class Extension {
        /// <summary>
        /// Get description string of an object's ToString() and GetType().Name
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDescription(this object key) => $"{key.ToString()} ({key.GetType().Name})";
    }
    public static class VisualElementExtension {
        public static void Padding(this VisualElement element, StyleLength top, StyleLength buttom, StyleLength left, StyleLength right) {
            element.style.paddingTop = top;
            element.style.paddingBottom = buttom;
            element.style.paddingLeft = left;
            element.style.paddingRight = right;
        }
        public static void Margin(this VisualElement element, StyleLength top, StyleLength buttom, StyleLength left, StyleLength right) {
            element.style.marginTop = top;
            element.style.marginBottom = buttom;
            element.style.marginLeft = left;
            element.style.marginRight = right;
        }
        public static void BorderWidth(this VisualElement element, StyleFloat top, StyleFloat buttom, StyleFloat left, StyleFloat right) {
            element.style.borderTopWidth = top;
            element.style.borderBottomWidth = buttom;
            element.style.borderLeftWidth = left;
            element.style.borderRightWidth = right;
        }
    }
}