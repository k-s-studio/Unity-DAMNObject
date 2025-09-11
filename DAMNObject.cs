using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.KsCode.DAMNObject {
    /// <summary>
    /// Access point of intantiated DamnModel
    /// </summary>
    public class DAMNObject : MonoBehaviour {//, IDictionary<object, Component> { //超麻煩欸?
        [SerializeField] private ComponentTable m_Table = new();
        public Component this[object o] => m_Table[o];
        public T GetComponent<T>(object i) where T : Component => this[i] as T;
        public IEnumerable<KeyValuePair<object, Component>> Entries => m_Table.Components.AsEnumerable();
        public (object key, Component value) TryAddItem { set => m_Table.TryAdd(value.key, value.value); }
        //private void Clear() => m_Table.Clear();

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
                // m_Components = new();
                // AddComponent() 和 Instantiate() 時都會先給欄位初始值，初始值是new()就執行constructor
                // 然後將有序列化的值覆寫回去
                // 在這裡初始化，複製的物件(未經過ToGameObject()創建)就會有原本的m_Entries但m_Components已經初始化，無法觸發Recover()
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
            // public void Clear() {
            //     m_Entries.Clear();
            //     m_Components = null;
            // }
        }
    }
}