using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.KsCode.DAMNObject.v1 {
    using ComponentList = ComponentNode;

    [Serializable]
    public class DAMNPrefab {
        [NonSerialized] public DamnModel model;
        [NonSerialized] public DamnConfig config;
        public DAMNObject Instantiate() => model.Instantiate(config);
    }

    [Serializable]
    public class DamnModel : ModelChild {
        //[SerializeField][HideInInspector] private Spine m_GameObjectCache;
        public DamnModel(string n = "GameObject") : base(n) {
            //m_GameObjectCache = null;
        }
        public DAMNObject Instantiate(DamnConfig config = null) {
            var (root, lst) = ToGameObject();
            var damnObject = root.gameObject.AddComponent<DAMNObject>();
            foreach (var p in lst.Where(e => e.id != null)) damnObject.TryAddItem = p;
            foreach (var t in damnObject.GetComponentsInChildren<Transform>().
                Select<Transform, (object, Component)>(t => (t.name, t))) damnObject.TryAddItem = t;
            config?.Invoke(damnObject);
            return damnObject;
        }
        public DAMNObject Instantiate(Action<DAMNObject> config) => Instantiate(new DamnConfig(config));
    }
    public class ModelChild {
        [NonSerialized] public string name;
        [NonSerialized] public ComponentList components; 
        [NonSerialized] public List<DamnModel> children;
        public ModelChild(string n = "GameObject") {
            name = n;
            components = new();
            children = new();
            // m_GameObjectCache = null;
        }

        /// <summary>
        /// AddComponent() is called here
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        protected (Transform node, IEnumerable<(object id, Component comp)> lst) ToGameObject(Transform parent = null) {
            Transform node = new GameObject(name).transform;
            if (parent != null) node.SetParent(parent);
            return (
                node,
                components.Select(e => (e.id, node.gameObject.AddComponent(e.type))).
                    Concat(children.SelectMany(c => c.ToGameObject(node).lst))
            );
        }
    }    
    public delegate void DamnConfig(DAMNObject s);
}