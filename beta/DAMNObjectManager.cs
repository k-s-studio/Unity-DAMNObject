using UnityEngine;
using System.Collections.Generic;

namespace Assets.KsCode.DAMNObject.beta {
    internal class DAMNObjectManager {
        private readonly static Dictionary<int, DAMNObject> m_SpineManager = new();
        internal static bool TryGetSpine(GameObject obj, out DAMNObject spine) {
            if (m_SpineManager.TryGetValue(obj.GetInstanceID(), out spine)) return spine;
            else spine = obj.GetComponent<DAMNObject>();
            if (spine) m_SpineManager.Add(obj.GetInstanceID(), spine);
            return spine;
        }
        internal static void Register(DAMNObject spine) => m_SpineManager.Add(spine.gameObject.GetInstanceID(), spine);
    }
    internal static partial class Extension {
        public static T GetFromModel<T>(this GameObject obj, object id) where T : Component => obj.GetFromModel(id) as T;
        public static Component GetFromModel(this GameObject obj, object id) {
            if (DAMNObjectManager.TryGetSpine(obj, out DAMNObject s)) return s[id];
            else Debug.LogError($"GameObject '{obj.name}' is not a DAMNObject");
            return null;
        }
    }
}