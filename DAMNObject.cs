using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.KsCode.DAMNObject {
    /// <summary>
    /// Access point of intantiated DamnModel
    /// </summary>
    public class DAMNObject : MonoBehaviour {
        [SerializeField] private ComponentTable m_Table = new();
        public Component this[object o] => m_Table[o];
        public T GetComponent<T>(object i) where T : Component => this[i] as T;
        public IEnumerable<KeyValuePair<object, Component>> Entries => m_Table.Components.AsEnumerable();
        public (object key, Component value) TryAddItem { set => m_Table.TryAdd(value.key, value.value); }

        [Serializable]
        private class ComponentTable {
            [SerializeField] private List<KeyCompPair> m_Entries;
            private Dictionary<object, Component> m_Components = null;
            public Dictionary<object, Component> Components {
                get {
                    if (m_Components == null) Recover();
                    return m_Components;
                }
            }
            public ComponentTable() {
                m_Entries = new();
            }
            public void TryAdd(object key, Component value) {
                Key k = new(key);
                if (m_Components == null) Recover();
                if (m_Components.TryAdd(k, value)) m_Entries.Add(new(k, value));
                else Debug.LogWarningFormat("Key '{}' is duplicate.", k);
            }
            private void Recover() {
                m_Components = new(m_Entries.Select(e => (KeyValuePair<object, Component>)e));
                //Debug.Log("Recovery!");
            }
            public void AddToList(KeyCompPair p) => m_Entries.Add(p);
            public Component this[object o] => Components[o];
        }
    }
}